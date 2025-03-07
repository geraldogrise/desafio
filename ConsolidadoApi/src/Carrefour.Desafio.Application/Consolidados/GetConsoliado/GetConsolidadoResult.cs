namespace Carrefour.Desafio.Application.Consolidados.GetConsolidado;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetConsolidadoResult
{
    public Guid Id { get; set; }
    public DateTime DataConsolidado { get; set; }
    public decimal ValorDebito { get; set; }
    public decimal ValorCredito { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
