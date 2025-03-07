using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Application.Users.DTOS;

namespace Carrefour.Desafio.Application.Users.GetAllUser
{
    /// <summary>
    /// Handler for processing GetAllUsersCommand requests
    /// </summary>
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, GetAllUsersResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetAllUsersHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetAllUsersCommand request
        /// </summary>
        /// <param name="request">The GetAllUsers command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated list of users</returns>
        public async Task<GetAllUsersResult> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetAllUsersValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var pagedUsers = await _userRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);

            var result = new GetAllUsersResult()
            {
                TotalItems = pagedUsers.TotalItems,
                TotalPages = pagedUsers.TotalPages,
                Data = _mapper.Map<List<UserDto>>(pagedUsers.Data.ToList())
            };
              
            
            return _mapper.Map<GetAllUsersResult>(result);
        }
    }
}
