using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Users.GetAllUser
{

    /// <summary>
    /// Validator for GetAllUsersCommand
    /// </summary>
    public class GetAllUsersValidator : AbstractValidator<GetAllUsersCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetAllUsersCommand
        /// </summary>
        public GetAllUsersValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0");

            RuleFor(x => x.Order)
                .NotEmpty()
                .WithMessage("Order field cannot be empty");
        }
    }
}
