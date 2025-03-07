using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Domain.Validation;
using FluentValidation;

namespace Carrefour.Desafio.Application.Users.UpdateUser
{
    /// <summary>
    /// Validator for UpdateUserCommand that defines validation rules for updating a user.
    /// </summary>
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(user => user.Id).NotEmpty().WithMessage("User ID is required.");
            RuleFor(user => user.Email).SetValidator(new EmailValidator());
            RuleFor(user => user.Username).NotEmpty().Length(3, 50);
            RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
            RuleFor(user => user.Role).NotEqual(UserRole.None);
        }
    }
}
