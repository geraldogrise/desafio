using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetLancamento;

/// <summary>
/// Validator for GetlancamentoRequest
/// </summary>
public class GetLancamentoRequestValidator : AbstractValidator<GetLancamentoRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    public GetLancamentoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Lancamento ID is required");
    }
}
