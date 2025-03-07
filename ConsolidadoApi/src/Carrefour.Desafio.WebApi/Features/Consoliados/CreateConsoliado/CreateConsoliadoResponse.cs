namespace Carrefour.Desafio.WebApi.Features.Consolidados.CreateConsolidado;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class CreateConsolidadoResponse
{
    public Guid Id { get; set; } 
    public DateTime DataConsolidado { get; set; }
    public decimal ValorDebito { get; set; } 
    public decimal ValorCredito { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }

}
