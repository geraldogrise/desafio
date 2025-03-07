using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Carrefour.Desafio.WebApi.Common;
using Carrefour.Desafio.WebApi.Features.Consolidados.CreateConsolidado;
using Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;
using Carrefour.Desafio.Application.Consolidados.CreateConsolidado;
using Carrefour.Desafio.Application.Consolidados.GetConsolidado;
using Carrefour.Desafio.Application.Consolidados.GetAllConsolidado;
using Carrefour.Desafio.WebApi.Features.Consolidados.GetAllConsolidado;
using Microsoft.AspNetCore.Authorization;
using Prometheus;


namespace Carrefour.Desafio.WebApi.Features.Consolidados;

/// <summary>
/// Controller for managing consolidado operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ConsolidadosController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ConsolidadosController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ConsolidadosController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new consolidado
    /// </summary>
    /// <param name="request">The consolidado creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created consolidado details</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateConsolidadoResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateConsolidado([FromBody] CreateConsolidadoRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new CreateConsolidadoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateConsolidadoCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Created(string.Empty, new ApiResponseWithData<CreateConsolidadoResponse>
            {
                Success = true,
                Message = "Consolidado created successfully",
                Data = _mapper.Map<CreateConsolidadoResponse>(response)
            });
        }
    }

  
    /// <summary>
    /// Retrieves a consolidado by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado details if found</returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetConsolidadoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConsolidado([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var request = new GetConsolidadoRequest { Id = id };
            var validator = new GetConsolidadoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetConsolidadoCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetConsolidadoResponse>
            {
                Success = true,
                Message = "Consolidado retrieved successfully",
                Data = _mapper.Map<GetConsolidadoResponse>(response)
            });
        }
    }


    /// <summary>
    /// Retrieves a consolidado by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the consolidado</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The consolidado details if found</returns>
    [Authorize]
    [HttpGet("date/{date}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetConsolidadoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConsolidadoByDate([FromRoute] DateTime date, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var request = new GetConsolidadoByDateRequest { Date = date };
            var validator = new GetConsolidadoByDateRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetConsolidadoByDateCommand>(request.Date);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetConsolidadoByDateResponse>
            {
                Success = true,
                Message = "Consolidado retrieved successfully",
                Data = _mapper.Map<GetConsolidadoByDateResponse>(response)
            });
        }
    }

    /// <summary>
    /// Retrieves all products with pagination
    /// </summary>
    /// <param name="request">The request containing pagination parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of products</returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllConsolidadosResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllConsolidados([FromQuery] GetAllConsolidadosRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new GetAllConsolidadosRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetAllConsolidadosCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            var mappedResponse = _mapper.Map<GetAllConsolidadosResponse>(response);
            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetAllConsolidadosResponse>
            {
                Success = true,
                Message = "Consolidados retrieved successfully",
                Data = mappedResponse
            });
        }
    }

}
