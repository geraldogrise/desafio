using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetAllConsolidado
{
    /// <summary>
    /// Validator for GetAllConsolidadosRequest.
    /// </summary>
    public class GetAllConsolidadosRequestValidator : AbstractValidator<GetAllConsolidadosRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetAllConsolidadosRequest.
        /// </summary>
        public GetAllConsolidadosRequestValidator()
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
