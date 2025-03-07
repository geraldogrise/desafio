using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;

namespace Carrefour.Desafio.Application.Consolidados.GetConsolidado;

/// <summary>
/// Handler for processing GetConsolidadoCommand requests
/// </summary>
public class GetConsoliadoByDateHandler : IRequestHandler<GetConsolidadoByDateCommand, GetConsolidadoByDateResult>
{
    private readonly IConsolidadoRepository _consolidadoRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetConsolidadoHandler
    /// </summary>
    /// <param name="consolidadoRepository">The consolidado repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetConsolidadoCommand</param>
    public GetConsoliadoByDateHandler(
        IConsolidadoRepository consolidadoRepository,
        IMapper mapper)
    {
        _consolidadoRepository = consolidadoRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetConsolidadoCommand request
    /// </summary>
    /// <param name="request">The GetConsolidado command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado details if found</returns>
    public async Task<GetConsolidadoByDateResult> Handle(GetConsolidadoByDateCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetConsolidadoByDateValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var consolidado = await _consolidadoRepository.GetByDateAsync(request.Date, cancellationToken);
        if (consolidado == null)
            throw new KeyNotFoundException($"Consolidado with Date {request.Date} not found");

        return _mapper.Map<GetConsolidadoByDateResult>(consolidado);
    }
}
