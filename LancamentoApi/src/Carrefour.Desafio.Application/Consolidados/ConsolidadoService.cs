using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Carrefour.Desafio.Application.Consolidados.Request;
using Carrefour.Desafio.Common.Validation;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Serilog;

namespace Carrefour.Desafio.Application.Consolidados
{
    public class ConsolidadoService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy;
        private readonly string _apiUrl;

        public static CreateConsolidadoCommand ConvertToConsolidado(Lancamento lancamento, CreateConsolidadoCommand? consolidado)
        {
            if(consolidado == null)
            {
                consolidado = new CreateConsolidadoCommand();
            }
            var valorDebito = lancamento.Tipo == TipoLancamento.DEBITO ? lancamento.ValorLancamento : 0;
            var valorCredito = lancamento.Tipo == TipoLancamento.CREDITO ? lancamento.ValorLancamento : 0;
            var saldoTotal = lancamento.Tipo == TipoLancamento.CREDITO ? consolidado.SaldoFinal + valorCredito : consolidado.SaldoFinal - valorDebito;
            
            return new CreateConsolidadoCommand
            {
                Id = consolidado.Id,
                DataConsolidado = lancamento.DataLancamento,
                ValorDebito = consolidado.ValorDebito + valorDebito,
                ValorCredito = consolidado.ValorCredito + valorCredito,
                SaldoFinal = saldoTotal,
                CreatedAt = DateTime.UtcNow
            };
        }

        public ConsolidadoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["Consolidado:ConsolidadoApiUrl"] ?? throw new ArgumentNullException("A URL da API não foi configurada!");

            _circuitBreakerPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 3,  
                    durationOfBreak: TimeSpan.FromSeconds(30) 
                );
        }


        public async Task<CreateConsolidadoCommand?> GetConsolidadoByDateAsync(DateTime date, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            var response = await client.GetAsync($"{_apiUrl}/date/{date:yyyy-MM-ddTHH:mm:ss}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ApiResponseWithData<ResponseData<CreateConsolidadoCommand>>>(json);

            return obj?.Data?.Data;  
        }

        public async Task<bool> EnviarConsolidadoAsync(CreateConsolidadoCommand consolidado, string token)
        {
            var jsonContent = JsonConvert.SerializeObject(consolidado);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            try
            {
                var response = await _circuitBreakerPolicy.ExecuteAsync(async () =>
                {
                    var result = await _httpClient.PostAsync(_apiUrl, content);
                    result.EnsureSuccessStatusCode();
                    return result;
                });

                return response.IsSuccessStatusCode;
            }
            catch (BrokenCircuitException)
            {
                Log.Information("⚠ API de Consolidados está indisponível. Tentaremos novamente mais tarde.");
                return false;
            }
        }

        private class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
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
    }


}
