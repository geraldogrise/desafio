namespace Carrefour.Desafio.Application.Consolidados.CreateConsolidado;

/// <summary>
/// Represents the response returned after successfully creating a new consolidado.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created consolidado,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateConsolidadoResult
{
    public Guid Id { get; set; }
    public DateTime DataConsolidado { get; set; }
    public decimal ValorDebito { get; set; }
    public decimal ValorCredito { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
