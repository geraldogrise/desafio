namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;

/// <summary>
/// Request model for getting a consolidado by ID
/// </summary>
public class GetConsolidadoRequest
{
    /// <summary>
    /// The unique identifier of the consolidado to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
