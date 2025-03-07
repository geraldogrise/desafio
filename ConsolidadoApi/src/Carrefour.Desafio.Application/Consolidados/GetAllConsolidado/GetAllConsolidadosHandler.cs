using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Application.Consolidados.DTOS;
using Carrefour.Desafio.Domain.Repositories;

namespace Carrefour.Desafio.Application.Consolidados.GetAllConsolidado
{
    /// <summary>
    /// Handler for processing GetAllConsolidadosCommand requests
    /// </summary>
    public class GetAllConsolidadosHandler : IRequestHandler<GetAllConsolidadosCommand, GetAllConsolidadosResult>
    {
        private readonly IConsolidadoRepository _consolidadoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetAllConsolidadosHandler
        /// </summary>
        /// <param name="consolidadoRepository">The consolidado repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetAllConsolidadosHandler(IConsolidadoRepository consolidadoRepository, IMapper mapper)
        {
            _consolidadoRepository = consolidadoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetAllConsolidadosCommand request
        /// </summary>
        /// <param name="request">The GetAllConsolidados command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated list of consolidados</returns>
        public async Task<GetAllConsolidadosResult> Handle(GetAllConsolidadosCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetAllConsolidadosValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var pagedConsolidados = await _consolidadoRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);

            var result = new GetAllConsolidadosResult()
            {
                TotalItems = pagedConsolidados.TotalItems,
                TotalPages = pagedConsolidados.TotalPages,
                Data = _mapper.Map<List<ConsolidadoDto>>(pagedConsolidados.Data.ToList())
            };
              
            
            return _mapper.Map<GetAllConsolidadosResult>(result);
        }
    }
}
