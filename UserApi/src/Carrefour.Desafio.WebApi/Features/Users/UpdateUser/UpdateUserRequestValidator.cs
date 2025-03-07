using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Domain.Validation;
using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Users.UpdateUser
{
    // <summary>
    /// Validator for UpdateUserRequest that defines validation rules for user update.
    /// </summary>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateUserRequestValidator with defined validation rules.
        /// </summary>
        public UpdateUserRequestValidator()
        {
            RuleFor(user => user.Id).NotEmpty().WithMessage("User ID is required");
            RuleFor(user => user.Email).SetValidator(new EmailValidator());
            RuleFor(user => user.Username).NotEmpty().Length(3, 50);
            RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
            RuleFor(user => user.Role).NotEqual(UserRole.None);
        }
    }
}
