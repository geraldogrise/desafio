using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using FluentValidation;
using Microsoft.Extensions.Options;
using NSubstitute;
using StackExchange.Redis;
using Xunit;
using Carrefour.Desafio.Application.Lancamentos.CreateLancamento;
using Carrefour.Desafio.Application.Consolidados;
using Carrefour.Desafio.Common.Kafka;
using Carrefour.Desafio.Common.Redis;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Domain.Entities;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Configuration;

namespace Carrefour.Desafio.Unit.Application.Lancamentos;
public class CreateLancamentoHandlerTests
{
    private readonly ILancamentoRepository _lancamentoRepository = Substitute.For<ILancamentoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    // Dependências do ConsolidadoService
    private readonly HttpClient _httpClient = Substitute.For<HttpClient>();
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();

    private readonly ConsolidadoService _consolidadoService;
    private readonly IProducer<string, string> _kafkaProducer = Substitute.For<IProducer<string, string>>();
    private readonly IConnectionMultiplexer _redisConnection = Substitute.For<IConnectionMultiplexer>();
    private readonly IDatabase _redisDatabase = Substitute.For<IDatabase>();
    private readonly IOptions<KafkaSettings> _kafkaSettings = Substitute.For<IOptions<KafkaSettings>>();
    private readonly IOptions<RedisSettings> _redisSettings = Substitute.For<IOptions<RedisSettings>>();

    public CreateLancamentoHandlerTests()
    {
        _redisConnection.GetDatabase().Returns(_redisDatabase);
        _kafkaSettings.Value.Returns(new KafkaSettings { TopicLancamentos = "test-topic" });
        _redisSettings.Value.Returns(new RedisSettings { Database = 0 });

        // Criando o mock do ConsolidadoService corretamente
        _consolidadoService = Substitute.ForPartsOf<ConsolidadoService>(_httpClient, _configuration);
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var handler = new CreateLancamentoHandler(_lancamentoRepository, _mapper, _consolidadoService,
                                                   _kafkaProducer, _redisConnection, _kafkaSettings, _redisSettings);
        var invalidCommand = new CreateLancamentoCommand();
        var cancellationToken = new CancellationToken();

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(invalidCommand, cancellationToken));
    }

    [Fact]
    public async Task Handle_Should_Create_Lancamento_And_Return_Result()
    {
        // Arrange
        var handler = new CreateLancamentoHandler(_lancamentoRepository, _mapper, _consolidadoService,
                                                   _kafkaProducer, _redisConnection, _kafkaSettings, _redisSettings);
        var command = new CreateLancamentoCommand
        {
            Token = "test-token",
            DataLancamento = DateTime.UtcNow,
            ValorLancamento = 100,
            Tipo = Carrefour.Desafio.Domain.Enums.TipoLancamento.CREDITO
        };
        var cancellationToken = new CancellationToken();
        var lancamento = new Lancamento();
        var createdLancamento = new Lancamento();
        var expectedResult = new CreateLancamentoResult();

        _mapper.Map<Lancamento>(command).Returns(lancamento);
        _lancamentoRepository.CreateAsync(lancamento, cancellationToken).Returns(Task.FromResult(createdLancamento));
        _mapper.Map<CreateLancamentoResult>(createdLancamento).Returns(expectedResult);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResult, result);
    }

  
}
