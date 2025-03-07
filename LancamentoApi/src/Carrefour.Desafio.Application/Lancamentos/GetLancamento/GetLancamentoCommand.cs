using MediatR;

namespace Carrefour.Desafio.Application.Lancamentos.GetLancamento;

/// <summary>
/// Command for retrieving a user by their ID
/// </summary>
public record GetLancamentoCommand : IRequest<GetLancamentoResult>
{
    /// <summary>
    /// The unique identifier of the user to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetUserCommand
    /// </summary>
    /// <param name="id">The ID of the user to retrieve</param>
    public GetLancamentoCommand(Guid id)
    {
        Id = id;
    }
}
