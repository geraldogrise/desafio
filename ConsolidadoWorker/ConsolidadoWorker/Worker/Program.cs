using Carrefour.Desafio.Worker.Services;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Worker.Kafka;
using Worker.Redis;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<KafkaSettings>(context.Configuration.GetSection("Kafka"));
        services.Configure<RedisSettings>(context.Configuration.GetSection("Redis"));

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
            return ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
        });

        // ✅ Corrigindo a injeção do HttpClient e ConsolidadoService
        services.AddHttpClient<ConsolidadoService>(); // Adiciona o HttpClient gerenciado pelo .NET
        services.AddSingleton<ConsolidadoService>();  // Registra o serviço corretamente

        services.AddHostedService<ConsolidadoKafkaConsumer>();
    })
    .Build();

await host.RunAsync();
