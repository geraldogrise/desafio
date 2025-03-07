using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Carrefour.Desafio.WebApi.Common;
using Carrefour.Desafio.WebApi.Features.Lancamentos.CreateLancamento;
using Carrefour.Desafio.WebApi.Features.Lancamentos.GetLancamento;
using Carrefour.Desafio.Application.Lancamentos.CreateLancamento;
using Carrefour.Desafio.Application.Lancamentos.GetLancamento;
using Carrefour.Desafio.Application.Lancamentos.DeleteLancamento;
using Carrefour.Desafio.Application.Lancamentos.UpdateLancamento;
using Carrefour.Desafio.WebApi.Features.Lancamentos.UpdateLancamento;
using Carrefour.Desafio.Application.Lancamentos.GetAllLancamento;
using Carrefour.Desafio.WebApi.Features.Lancamentos.GetAllLancamento;
using Microsoft.AspNetCore.Authorization;
using Prometheus;
using Carrefour.Desafio.WebApi.Features.Lancamentos.DeleteLancamento;
using System.Net;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos;

/// <summary>
/// Controller for managing lancamento operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LancamentosController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of LancamentosController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public LancamentosController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new lancamento
    /// </summary>
    /// <param name="request">The lancamento creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created lancamento details</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateLancamentoResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLancamento([FromBody] CreateLancamentoRequest request, [FromHeader(Name = "Authorization")] string authorization, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new CreateLancamentoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateLancamentoCommand>(request);
            command.Token = authorization;
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Created(string.Empty, new ApiResponseWithData<CreateLancamentoResponse>
            {
                Success = true,
                Message = "Lancamento created successfully",
                Data = _mapper.Map<CreateLancamentoResponse>(response)
            });
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateLancamentoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLancamento([FromRoute] Guid id, [FromBody] UpdateLancamentoRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new UpdateLancamentoRequestValidator();
            request.Id = id;
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateLancamentoCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<UpdateLancamentoResponse>
            {
                Success = true,
                Message = "Lancamento updated successfully",
                Data = _mapper.Map<UpdateLancamentoResponse>(response)
            });
        }
    }

    /// <summary>
    /// Retrieves a lancamento by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the lancamento</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The lancamento details if found</returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetLancamentoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLancamento([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var request = new GetLancamentoRequest { Id = id };
            var validator = new GetLancamentoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetLancamentoCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetLancamentoResponse>
            {
                Success = true,
                Message = "Lancamento retrieved successfully",
                Data = _mapper.Map<GetLancamentoResponse>(response)
            });
        }
    }

    /// <summary>
    /// Deletes a lancamento by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the lancamento to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the lancamento was deleted</returns>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLancamento([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var request = new DeleteLancamentoRequest { Id = id };
            var validator = new DeleteLancamentoRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteLancamentoCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<DeleteLancamentoResponse>
            {
                Success = true,
                Message = "Lancamento deleted successfully",
                Data = _mapper.Map<DeleteLancamentoResponse>(response)
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
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllLancamentosResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllLancamentos([FromQuery] GetAllLancamentosRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new GetAllLancamentosRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetAllLancamentosCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            var mappedResponse = _mapper.Map<GetAllLancamentosResponse>(response);
            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetAllLancamentosResponse>
            {
                Success = true,
                Message = "Lancamentos retrieved successfully",
                Data = mappedResponse
            });
        }
    }

}
