using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetAllLancamento
{
    /// <summary>
    /// Validator for GetAllLancamentosRequest.
    /// </summary>
    public class GetAllLancamentosRequestValidator : AbstractValidator<GetAllLancamentosRequest>
    {
        /// <summary>
        /// Initializes validation rules for GetAllLancamentosRequest.
        /// </summary>
        public GetAllLancamentosRequestValidator()
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
