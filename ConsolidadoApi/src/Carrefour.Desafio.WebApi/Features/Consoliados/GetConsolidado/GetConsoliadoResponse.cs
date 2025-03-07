namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class GetConsolidadoResponse
{
    public Guid Id { get; set; }
    public DateTime DataConsolidado { get; set; }
    public decimal ValorDebito { get; set; }
    public decimal ValorCredito { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
