using FluentValidation;

namespace Carrefour.Desafio.Application.Consolidados.GetConsolidado;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetConsolidadoByDateValidator : AbstractValidator<GetConsolidadoByDateCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetConsolidadoByDateValidator()
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
