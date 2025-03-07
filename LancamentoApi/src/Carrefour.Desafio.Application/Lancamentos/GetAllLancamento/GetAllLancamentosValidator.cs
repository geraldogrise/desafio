using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Lancamentos.GetAllLancamento
{

    /// <summary>
    /// Validator for GetAllLancamentosCommand
    /// </summary>
    public class GetAllLancamentosValidator : AbstractValidator<GetAllLancamentosCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetAllLancamentosCommand
        /// </summary>
        public GetAllLancamentosValidator()
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
