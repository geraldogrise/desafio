using AutoMapper;
using MediatR;
using FluentValidation;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Users.UpdateUser
{

    /// <summary>
    /// Handler for processing UpdateUserCommand requests.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of UpdateUserHandler.
        /// </summary>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the UpdateUserCommand request.
        /// </summary>
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingUser == null)
                throw new InvalidOperationException($"User with ID {command.Id} not found");

            // Atualiza os valores do usuário
            _mapper.Map(command, existingUser);

            var updatedUser = await _userRepository.UpdateAsync(command.Id, existingUser, cancellationToken);
            return _mapper.Map<UpdateUserResult>(updatedUser);
        }
    }
}
