using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.UpdateLancamento
{
    /// <summary>
    /// Represents a request to update an existing user in the system.
    /// </summary>
    public class UpdateLancamentoRequest
    {
        public Guid Id { get; set; }
        public DateTime DataLancamento { get; set; }

        public TipoLancamento Tipo { get; set; }

        public decimal ValorLancamento { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
