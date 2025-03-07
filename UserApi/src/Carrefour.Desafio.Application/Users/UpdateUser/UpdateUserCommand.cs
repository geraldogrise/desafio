using Carrefour.Desafio.Application.Users.DTOS;
using Carrefour.Desafio.Common.Validation;
using Carrefour.Desafio.Domain.Enums;
using MediatR;

namespace Carrefour.Desafio.Application.Users.UpdateUser
{
    /// <summary>
    /// Command for updating an existing user.
    /// </summary>
    public class UpdateUserCommand : IRequest<UpdateUserResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number for the user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Validates the command.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateUserCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
