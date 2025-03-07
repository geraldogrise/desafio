using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Worker.Kafka.Entity;

namespace Worker.Kafka
{
    public class ConsolidadoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ConsolidadoService> _logger;
        private readonly string _apiUrl;

        public ConsolidadoService(HttpClient httpClient, IConfiguration configuration, ILogger<ConsolidadoService> logger)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["Consolidado:ApiUrl"] ?? throw new ArgumentNullException("URL da API de Consolidados não configurada.");
            _logger = logger;
        }

        public async Task<ConsolidadoEvent?> GetConsolidadoByDateAsync(DateTime date, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_apiUrl}/date/{date:yyyy-MM-ddTHH:mm:ss}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ApiResponseWithData<ResponseData<ConsolidadoEvent>>>(json);

            return obj?.Data?.Data;
         }

        public async Task<bool> EnviarConsolidadoAsync(ConsolidadoEvent consolidado, string token)
        {
            var jsonContent = JsonConvert.SerializeObject(consolidado);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync(_apiUrl, content);
            return response.IsSuccessStatusCode;
        }

        private class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public IEnumerable<object> Errors { get; set; } = [];
        }

        // Ajustado para corresponder à estrutura JSON correta
        private class ApiResponseWithData<T> : ApiResponse
        {
            public T? Data { get; set; }
        }

        // Representa a estrutura do campo "data" dentro do JSON de resposta
        private class ResponseData<T>
        {
            public T? Data { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public IEnumerable<string>? Errors { get; set; }
        }

        public static ConsolidadoEvent ConvertToConsolidado(Consolidado consolidadoKafka, ConsolidadoEvent? consolidado)
        {
            if (consolidado == null)
            {
                consolidado = new ConsolidadoEvent();
            }
            return new ConsolidadoEvent
            {
                Id = consolidado?.Id ?? Guid.NewGuid(),
                DataConsolidado = consolidadoKafka.DataConsolidado,
                ValorDebito = consolidado.ValorDebito + consolidadoKafka.ValorDebito,
                ValorCredito = consolidado.ValorCredito + consolidadoKafka.ValorCredito,
                SaldoFinal = consolidado.SaldoFinal + consolidadoKafka.ValorCredito - consolidadoKafka.ValorDebito,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
