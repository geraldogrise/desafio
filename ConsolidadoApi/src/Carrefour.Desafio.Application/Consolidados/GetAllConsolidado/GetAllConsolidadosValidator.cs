using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Consolidados.GetAllConsolidado
{

    /// <summary>
    /// Validator for GetAllConsolidadosCommand
    /// </summary>
    public class GetAllConsolidadosValidator : AbstractValidator<GetAllConsolidadosCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetAllConsolidadosCommand
        /// </summary>
        public GetAllConsolidadosValidator()
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
