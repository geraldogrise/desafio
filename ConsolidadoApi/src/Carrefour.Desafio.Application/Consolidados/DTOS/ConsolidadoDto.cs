namespace Carrefour.Desafio.Application.Consolidados.DTOS
{

    /// <summary>
    /// Represents a consolidado in the GetAllConsolidados response
    /// </summary>
    public class ConsolidadoDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataConsolidado { get; set; }
        public decimal ValorDebito { get; set; } = 0;
        public decimal ValorCredito { get; set; } = 0;
        public decimal SaldoFinal { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
