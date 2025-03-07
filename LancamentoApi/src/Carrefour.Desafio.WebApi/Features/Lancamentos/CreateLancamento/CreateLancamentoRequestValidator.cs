using FluentValidation;
using Carrefour.Desafio.Domain.Enums;


namespace Carrefour.Desafio.WebApi.Features.Lancamentos.CreateLancamento;

/// <summary>
/// Validator for CreateUserRequest that defines validation rules for user creation.
/// </summary>
public class CreateLancamentoRequestValidator : AbstractValidator<CreateLancamentoRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public CreateLancamentoRequestValidator()
    {
        RuleFor(lancamento => lancamento.DataLancamento)
           .NotEmpty()
           .LessThanOrEqualTo(DateTime.UtcNow)
           .WithMessage("A data do lançamento não pode ser futura.");

        RuleFor(lancamento => lancamento.Tipo)
            .NotEmpty()
            .Must(tipo => tipo == TipoLancamento.DEBITO || tipo == TipoLancamento.CREDITO)
            .WithMessage("O tipo de lançamento deve ser 'DEBITO' ou 'CREDITO'.");

        RuleFor(lancamento => lancamento.ValorLancamento)
            .GreaterThan(0)
            .WithMessage("O valor do lançamento deve ser maior que zero.");

        RuleFor(lancamento => lancamento.Descricao)
            .MaximumLength(255)
            .WithMessage("A descrição do lançamento deve ter no máximo 255 caracteres.");

        RuleFor(lancamento => lancamento.Categoria)
            .MaximumLength(100)
            .WithMessage("A categoria do lançamento deve ter no máximo 100 caracteres.");
    }
}