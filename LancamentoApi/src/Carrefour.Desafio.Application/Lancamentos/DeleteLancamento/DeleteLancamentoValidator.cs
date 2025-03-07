using FluentValidation;

namespace Carrefour.Desafio.Application.Lancamentos.DeleteLancamento;

/// <summary>
/// Validator for DeleteUserCommand
/// </summary>
public class DeleteLancamentoValidator : AbstractValidator<DeleteLancamentoCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteUserCommand
    /// </summary>
    public DeleteLancamentoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Lancamento ID is required");
    }
}
