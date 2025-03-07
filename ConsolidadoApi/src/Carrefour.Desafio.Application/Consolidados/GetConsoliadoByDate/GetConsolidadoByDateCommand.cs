using MediatR;

namespace Carrefour.Desafio.Application.Consolidados.GetConsolidado;

/// <summary>
/// Command for retrieving a consolidado by their ID
/// </summary>
public record GetConsolidadoByDateCommand : IRequest<GetConsolidadoByDateResult>
{
    /// <summary>
    /// The unique identifier of the consolidado to retrieve
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// Initializes a new instance of GetUserCommand
    /// </summary>
    /// <param name="id">The ID of the consolidado to retrieve</param>
    public GetConsolidadoByDateCommand(DateTime date)
    {
        Date = date;
    }
}
