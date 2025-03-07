using MediatR;

namespace Carrefour.Desafio.Application.Lancamentos.DeleteLancamento;

/// <summary>
/// Command for deleting a user
/// </summary>
public record DeleteLancamentoCommand : IRequest<DeleteLancamentoResult>
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteUserCommand
    /// </summary>
    /// <param name="id">The ID of the user to delete</param>
    public DeleteLancamentoCommand(Guid id)
    {
        Id = id;
    }
}
