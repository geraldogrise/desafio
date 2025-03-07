using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;
using Worker.Kafka;
using Worker.Kafka.Entity;
using Worker.Redis;

namespace Carrefour.Desafio.Worker.Services
{
    public class ConsolidadoKafkaConsumer : BackgroundService
    {
        private readonly KafkaSettings _kafkaSettings;
        private readonly ILogger<ConsolidadoKafkaConsumer> _logger;
        private readonly ConsolidadoService _consolidadoService;
        private readonly IDatabase _redisDatabase;

        public ConsolidadoKafkaConsumer(
            IOptions<KafkaSettings> kafkaSettings,
            ILogger<ConsolidadoKafkaConsumer> logger,
            ConsolidadoService consolidadoService,
            IConnectionMultiplexer redisConnection,
            IOptions<RedisSettings> redisSettings)
        {
            if (kafkaSettings?.Value == null)
            {
                throw new ArgumentNullException(nameof(kafkaSettings), "KafkaSettings não foi carregado corretamente!");
            }
            _kafkaSettings = kafkaSettings.Value;
            _logger = logger;
            _consolidadoService = consolidadoService;
            _redisDatabase = redisConnection.GetDatabase(redisSettings.Value.Database);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = _kafkaSettings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false,
            };

            using var consumer = new ConsumerBuilder<string, string>(config)
                                .SetKeyDeserializer(Deserializers.Utf8)
                                .SetValueDeserializer(Deserializers.Utf8)
                                .Build();
            consumer.Subscribe(_kafkaSettings.Topic);

            _logger.LogInformation("📢 Worker Kafka iniciado, escutando mensagens...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(stoppingToken);
                        var message = consumeResult.Message.Value;
                        var eventId = consumeResult.Message.Key;

                        _logger.LogInformation("📩 Mensagem recebida: {Message}", message);

                        if (await MensagemJaProcessada(eventId))
                        {
                            _logger.LogWarning("🔄 Mensagem duplicada detectada, ignorando: {EventId}", eventId);
                            continue;
                        }

                        Consolidado? consolidadoKafka;
                        try
                        {
                            consolidadoKafka = JsonSerializer.Deserialize<Consolidado>(message);
                        }
                        catch (JsonException ex)
                        {
                            _logger.LogWarning("⚠ Erro ao desserializar mensagem: {Message}", ex.Message);
                            continue;
                        }

                        if (consolidadoKafka is null)
                        {
                            _logger.LogWarning("⚠ Mensagem inválida ou nula!");
                            continue;
                        }

                        await ProcessarLancamento(consolidadoKafka, eventId);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("❌ Erro ao processar mensagem: {Message}", ex.Message);
                    }
                }
            }
            finally
            {
                consumer.Close();
            }
        }

        private async Task ProcessarLancamento(Consolidado consolidadoKafka, string eventId)
        {
            try
            {
                

                var consolidado = await _consolidadoService.GetConsolidadoByDateAsync(consolidadoKafka.DataConsolidado, consolidadoKafka.Token.Replace("Bearer ", ""));
                var consolidadoConvertido = ConsolidadoService.ConvertToConsolidado(consolidadoKafka, consolidado);
                var sucesso = await _consolidadoService.EnviarConsolidadoAsync(consolidadoConvertido, consolidadoKafka.Token.Replace("Bearer ", ""));
                await RegistrarMensagemProcessada(eventId);
                if (sucesso)
                {
                    _logger.LogInformation("✅ Consolidação enviada com sucesso para {Data consolidação}", consolidadoKafka.DataConsolidado);
                }
                else
                {
                    _logger.LogWarning("⚠ Consolidação falhou para {Data consolidação}", consolidadoKafka.DataConsolidado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("❌ Erro ao processar o lançamento: {Message}", ex.Message);
            }
        }

        private async Task<bool> MensagemJaProcessada(string eventId)
        {
            var chaveRedis = $"evento:{eventId}";
            return await _redisDatabase.KeyExistsAsync(chaveRedis);
        }

        private async Task RegistrarMensagemProcessada(string eventId)
        {
            var chaveRedis = $"evento:{eventId}";
            await _redisDatabase.StringSetAsync(chaveRedis, "processed", TimeSpan.FromHours(24));
        }
    }
}
