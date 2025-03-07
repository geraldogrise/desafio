using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;

namespace Carrefour.Desafio.Application.Lancamentos.GetLancamento;

/// <summary>
/// Handler for processing GetLancamentoCommand requests
/// </summary>
public class GetLancamentoHandler : IRequestHandler<GetLancamentoCommand, GetLancamentoResult>
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetLancamentoHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetLancamentoCommand</param>
    public GetLancamentoHandler(
        ILancamentoRepository lancamentoRepository,
        IMapper mapper)
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetLancamentoCommand request
    /// </summary>
    /// <param name="request">The GetLancamento command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<GetLancamentoResult> Handle(GetLancamentoCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetLancamentoValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = await _lancamentoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
            throw new KeyNotFoundException($"Lancamento with ID {request.Id} not found");

        return _mapper.Map<GetLancamentoResult>(user);
    }
}
