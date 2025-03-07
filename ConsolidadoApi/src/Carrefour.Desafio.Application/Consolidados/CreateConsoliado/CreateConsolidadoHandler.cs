using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Common.Security;


namespace Carrefour.Desafio.Application.Consolidados.CreateConsolidado;

/// <summary>
/// Handler for processing CreateConsolidadoCommand requests
/// </summary>
public class CreateConsolidadoHandler : IRequestHandler<CreateConsolidadoCommand, CreateConsolidadoResult>
{
    private readonly IConsolidadoRepository _consolidadoRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateConsolidadoHandler
    /// </summary>
    /// <param name="consolidadoRepository">The consolidado repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateConsolidadoCommand</param>
    public CreateConsolidadoHandler(IConsolidadoRepository consolidadoRepository, IMapper mapper)
    {
        _consolidadoRepository = consolidadoRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateConsolidadoCommand request
    /// </summary>
    /// <param name="command">The CreateConsolidado command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created consolidado details</returns>
    public async Task<CreateConsolidadoResult> Handle(CreateConsolidadoCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateConsolidadoCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

    
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var consolidadoCommand = _mapper.Map<Consolidado>(command);
        Consolidado consolidado = new Consolidado();
        var consolidated = await _consolidadoRepository.GetByDateAsync(command.DataConsolidado);
        if (consolidated != null)
        {
            consolidado = await _consolidadoRepository.UpdateAsync(consolidated.Id, consolidadoCommand, cancellationToken);
        }
        else 
        {
            consolidado = await _consolidadoRepository.CreateAsync(consolidadoCommand, cancellationToken);
        }

        var result = _mapper.Map<CreateConsolidadoResult>(consolidado);
        return result;
    }
}
