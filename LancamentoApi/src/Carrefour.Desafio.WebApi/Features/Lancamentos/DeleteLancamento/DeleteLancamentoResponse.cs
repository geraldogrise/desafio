using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.DeleteLancamento
{
    public class DeleteLancamentoResponse
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
