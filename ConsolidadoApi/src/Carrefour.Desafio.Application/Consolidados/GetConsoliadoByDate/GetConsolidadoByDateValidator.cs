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
                .NotEmpty().WithMessage("A data � obrigat�ria.")
                .Must(BeAValidDate).WithMessage("A data informada n�o � v�lida.");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date != default(DateTime);
    }
}
