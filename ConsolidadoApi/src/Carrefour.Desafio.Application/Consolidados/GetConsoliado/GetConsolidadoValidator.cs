using FluentValidation;

namespace Carrefour.Desafio.Application.Consolidados.GetConsolidado;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetConsolidadoValidator : AbstractValidator<GetConsolidadoCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetConsolidadoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Consolidado ID is required");
    }
}
