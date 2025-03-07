using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Users.GetAllUser
{
    /// <summary>
    /// Validator for GetAllUsersRequest.
    /// </summary>
    public class GetAllUsersRequestValidator : AbstractValidator<GetAllUsersRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetAllUsersRequest.
        /// </summary>
        public GetAllUsersRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0");
        }
    }
}
