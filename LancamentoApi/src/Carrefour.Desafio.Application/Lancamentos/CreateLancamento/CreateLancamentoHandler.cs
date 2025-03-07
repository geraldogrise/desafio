using MediatR;
using Serilog;
using AutoMapper;
using Confluent.Kafka;
using Newtonsoft.Json;
using FluentValidation;
using StackExchange.Redis;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Application.Consolidados;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core.Tokenizer;
using Carrefour.Desafio.Application.Consolidados.Request;
using Carrefour.Desafio.Common.Kafka;
using Microsoft.Extensions.Options;
using Carrefour.Desafio.Common.Redis;


namespace Carrefour.Desafio.Application.Lancamentos.CreateLancamento;

/// <summary>
/// Handler for processing CreateLancamentoCommand requests
/// </summary>
public class CreateLancamentoHandler : IRequestHandler<CreateLancamentoCommand, CreateLancamentoResult>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMapper _mapper;
    private readonly ConsolidadoService _consolidadoService;
    private readonly IProducer<string, string> _kafkaProducer;
    private readonly IDatabase _redisDatabase;
    private readonly KafkaSettings _kafkaSettings;


    /// <summary>
    /// Initializes a new instance of CreateLancamentoHandler
    /// </summary>
    /// <param name="lancamentoRepository">The lancamento repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateLancamentoCommand</param>
    public CreateLancamentoHandler(ILancamentoRepository lancamentoRepository,
                                   IMapper mapper,
                                   ConsolidadoService consolidadoService,
                                   IProducer<string, string> kafkaProducer,
                                   IConnectionMultiplexer redisConnection,
                                   IOptions<KafkaSettings> kafkaSettings,
                                   IOptions<RedisSettings> redisSettings) 
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
        _consolidadoService = consolidadoService;
        _kafkaProducer = kafkaProducer;
        _kafkaSettings = kafkaSettings.Value;
        _redisDatabase = redisConnection.GetDatabase(redisSettings.Value.Database);
    }

    /// <summary>
    /// Handles the CreateLancamentoCommand request
    /// </summary>
    /// <param name="command">The CreateLancamento command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created lancamento details</returns>
    public async Task<CreateLancamentoResult> Handle(CreateLancamentoCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateLancamentoCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var lancamento = _mapper.Map<Lancamento>(command);

        var createdLancamento = await _lancamentoRepository.CreateAsync(lancamento, cancellationToken);
        await EnviarConsolidadoAsync(lancamento, command.Token);
        await EnviarConsolidadoKafkaAsync(lancamento, command.Token);
        var result = _mapper.Map<CreateLancamentoResult>(createdLancamento);
        return result;
    }

    private async Task EnviarConsolidadoAsync(Lancamento lancamento, string token)
    {
        try
        {
            var lancamentoEvent = obterLancamentoEvent(lancamento, token);
            var consolidado = await _consolidadoService.GetConsolidadoByDateAsync(lancamento.DataLancamento, token);
            if (consolidado != null)
            {
                var consolidadoCommand = ConsolidadoService.ConvertToConsolidado(lancamento, consolidado);
                await _consolidadoService.EnviarConsolidadoAsync(consolidadoCommand, token);
                await RegistrarMensagem(lancamentoEvent);
            }
           

        }
        catch (Exception ex)
        {
            Log.Information("Não foi possivel enviar o lancamento para ser consolidada " + ex.Message);
        }

    }

    private LancamentoEvent obterLancamentoEvent(Lancamento lancamento, string token)
    {
        var eventId = $"{lancamento.Id}-{lancamento.DataLancamento:yyyyMMdd}";
        return new LancamentoEvent
        {

            EventId = eventId,
            Id = lancamento.Id,
            DataConsolidado = lancamento.DataLancamento,
            ValorDebito = lancamento.Tipo == Domain.Enums.TipoLancamento.DEBITO ? lancamento.ValorLancamento : 0,
            ValorCredito = lancamento.Tipo == Domain.Enums.TipoLancamento.CREDITO ? lancamento.ValorLancamento : 0,
            Token = token
        };
    }

    private async Task RegistrarMensagem(LancamentoEvent lancamentoEvent)
    {
        var jsonMessage = JsonConvert.SerializeObject(lancamentoEvent);
        var messageHash = ComputeHash(jsonMessage);
        await RegistrarMensagemProcessada(lancamentoEvent.EventId, messageHash);
    }
    private async Task EnviarConsolidadoKafkaAsync(Lancamento lancamento, string token)
    {
        var lancamentoEvent = obterLancamentoEvent(lancamento, token);
        var jsonMessage = JsonConvert.SerializeObject(lancamentoEvent);
        var messageHash = ComputeHash(jsonMessage); // Gera um hash único da mensagem

        try
        {
            // Verifica se essa mensagem já foi processada antes de enviar
            if (await MensagemJaProcessada(lancamentoEvent.EventId, messageHash))
            {
                Log.Warning("🔄 Evento já processado anteriormente: {EventId}", lancamentoEvent.EventId);
                return;
            }

            await _kafkaProducer.ProduceAsync(_kafkaSettings.TopicLancamentos, new Message<string, string>
            {
                Key = lancamentoEvent.EventId,
                Value = jsonMessage
            });

   
            Log.Information("📤 Evento de lançamento publicado no Kafka: {LancamentoId}", lancamento.Id);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "❌ Erro ao publicar evento de consolidação no Kafka");
        }
    }

    /// <summary>
    /// Gera um hash SHA256 para identificar mensagens duplicadas.
    /// </summary>
    private static string ComputeHash(string input)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Verifica no Redis se o evento já foi processado
    /// </summary>
    private async Task<bool> MensagemJaProcessada(string eventId, string messageHash)
    {
        var chaveRedis = $"evento:{eventId}";
        var hashSalvo = await _redisDatabase.StringGetAsync(chaveRedis);
        return !hashSalvo.IsNullOrEmpty && hashSalvo.ToString() == messageHash;
    }

    /// <summary>
    /// Registra o evento no Redis para evitar duplicação
    /// </summary>
    private async Task RegistrarMensagemProcessada(string eventId, string messageHash)
    {
        var chaveRedis = $"evento:{eventId}";
        await _redisDatabase.StringSetAsync(chaveRedis, messageHash, TimeSpan.FromHours(24));
    }

}