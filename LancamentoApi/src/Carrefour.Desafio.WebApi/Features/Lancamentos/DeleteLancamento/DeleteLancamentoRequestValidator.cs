using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.DeleteLancamento;

/// <summary>
/// Validator for DeleteUserRequest
/// </summary>
public class DeleteLancamentoRequestValidator : AbstractValidator<DeleteLancamentoRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteUserRequest
    /// </summary>
    public DeleteLancamentoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Lancamento ID is required");
    }
}
