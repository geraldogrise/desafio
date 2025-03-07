namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetLancamento;

/// <summary>
/// Request model for getting a lancamento by ID
/// </summary>
public class GetLancamentoRequest
{
    /// <summary>
    /// The unique identifier of the lancamento to retrieve
    /// </summary>
    public Guid Id { get; set; }
}
