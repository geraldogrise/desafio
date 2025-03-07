using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;

/// <summary>
/// Validator for GetconsolidadoRequest
/// </summary>
public class GetConsolidadoRequestValidator : AbstractValidator<GetConsolidadoRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    public GetConsolidadoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Consolidado ID is required");
    }
}
