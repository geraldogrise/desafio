using MediatR;
using AutoMapper;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;


namespace Carrefour.Desafio.Application.Lancamentos.DeleteLancamento;

/// <summary>
/// Handler for processing DeleteLancamentosCommand requests
/// </summary>
public class DeleteLancamentoHandler : IRequestHandler<DeleteLancamentoCommand, DeleteLancamentoResult>
{
    private readonly IMapper _mapper;
    private readonly ILancamentoRepository _lancamentoRepository;

    /// <summary>
    /// Initializes a new instance of DeleteLancamentosHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="validator">The validator for DeleteLancamentosCommand</param>
    public DeleteLancamentoHandler(
        IMapper mapper,
        ILancamentoRepository lancamentoRepository)
    {
        _mapper = mapper;
        _lancamentoRepository = lancamentoRepository;
    }

    /// <summary>
    /// Handles the DeleteLancamentosCommand request
    /// </summary>
    /// <param name="request">The DeleteLancamentos command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteLancamentoResult> Handle(DeleteLancamentoCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLancamentoValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _lancamentoRepository.GetByIdAsync(request.Id, cancellationToken);

        var success = await _lancamentoRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Lancamentos with ID {request.Id} not found");

        return _mapper.Map<DeleteLancamentoResult>(user);
    }
}
