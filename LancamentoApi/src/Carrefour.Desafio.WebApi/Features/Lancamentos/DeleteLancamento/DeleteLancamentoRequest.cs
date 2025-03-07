namespace Carrefour.Desafio.WebApi.Features.Lancamentos.DeleteLancamento;

/// <summary>
/// Request model for deleting a user
/// </summary>
public class DeleteLancamentoRequest
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; set; }
}
