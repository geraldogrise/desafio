using FluentValidation;

namespace Carrefour.Desafio.Application.Lancamentos.GetLancamento;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetLancamentoValidator : AbstractValidator<GetLancamentoCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetLancamentoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Lancamento ID is required");
    }
}
