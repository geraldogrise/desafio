using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;

/// <summary>
/// Validator for GetconsolidadoRequest
/// </summary>
public class GetConsolidadoByDateRequestValidator : AbstractValidator<GetConsolidadoByDateRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    public GetConsolidadoByDateRequestValidator()
    {
        RuleFor(x => x.Date)
                .NotEmpty().WithMessage("A data é obrigatória.")
                .Must(BeAValidDate).WithMessage("A data informada não é válida.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date != default(DateTime);
    }
}
