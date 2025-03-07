using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Application.Lancamentos.DTOS;
using Carrefour.Desafio.Domain.Repositories;

namespace Carrefour.Desafio.Application.Lancamentos.GetAllLancamento
{
    /// <summary>
    /// Handler for processing GetAllLancamentosCommand requests
    /// </summary>
    public class GetAllLancamentosHandler : IRequestHandler<GetAllLancamentosCommand, GetAllLancamentosResult>
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetAllLancamentosHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetAllLancamentosHandler(ILancamentoRepository lancamentoRepository, IMapper mapper)
        {
            _lancamentoRepository = lancamentoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetAllLancamentosCommand request
        /// </summary>
        /// <param name="request">The GetAllLancamentos command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated list of users</returns>
        public async Task<GetAllLancamentosResult> Handle(GetAllLancamentosCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetAllLancamentosValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var pagedLancamentos = await _lancamentoRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);

            var result = new GetAllLancamentosResult()
            {
                TotalItems = pagedLancamentos.TotalItems,
                TotalPages = pagedLancamentos.TotalPages,
                Data = _mapper.Map<List<LancamentoDto>>(pagedLancamentos.Data.ToList())
            };
              
            
            return _mapper.Map<GetAllLancamentosResult>(result);
        }
    }
}
