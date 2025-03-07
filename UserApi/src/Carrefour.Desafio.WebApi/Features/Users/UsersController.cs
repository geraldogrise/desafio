using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Carrefour.Desafio.WebApi.Common;
using Carrefour.Desafio.WebApi.Features.Users.CreateUser;
using Carrefour.Desafio.WebApi.Features.Users.GetUser;
using Carrefour.Desafio.WebApi.Features.Users.DeleteUser;
using Carrefour.Desafio.Application.Users.CreateUser;
using Carrefour.Desafio.Application.Users.GetUser;
using Carrefour.Desafio.Application.Users.DeleteUser;
using Carrefour.Desafio.Application.Users.UpdateUser;
using Carrefour.Desafio.WebApi.Features.Users.UpdateUser;
using Carrefour.Desafio.Application.Users.GetAllUser;
using Carrefour.Desafio.WebApi.Features.Users.GetAllUser;
using Microsoft.AspNetCore.Authorization;
using Prometheus;

namespace Carrefour.Desafio.WebApi.Features.Users;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new CreateUserRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateUserCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Created(string.Empty, new ApiResponseWithData<CreateUserResponse>
            {
                Success = true,
                Message = "User created successfully",
                Data = _mapper.Map<CreateUserResponse>(response)
            });
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new UpdateUserRequestValidator();
            request.Id = id;
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateUserCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<UpdateUserResponse>
            {
                Success = true,
                Message = "User updated successfully",
                Data = _mapper.Map<UpdateUserResponse>(response)
            });
        }
    }

    /// <summary>
    /// Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var request = new GetUserRequest { Id = id };
            var validator = new GetUserRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetUserCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetUserResponse>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = _mapper.Map<GetUserResponse>(response)
            });
        }
    }

    /// <summary>
    /// Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var request = new DeleteUserRequest { Id = id };
            var validator = new DeleteUserRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteUserCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<DeleteUserResponse>
            {
                Success = true,
                Message = "User deleted successfully",
                Data = _mapper.Map<DeleteUserResponse>(response)
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
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllUsersResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        using (var requestTimer = RequestDuration.NewTimer())
        using (var processingTimer = ProcessingLatency.NewTimer())
        {
            var validator = new GetAllUsersRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetAllUsersCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            var mappedResponse = _mapper.Map<GetAllUsersResponse>(response);
            RequestCounter.Inc();
            return Ok(new ApiResponseWithData<GetAllUsersResponse>
            {
                Success = true,
                Message = "Users retrieved successfully",
                Data = mappedResponse
            });
        }
    }

}
