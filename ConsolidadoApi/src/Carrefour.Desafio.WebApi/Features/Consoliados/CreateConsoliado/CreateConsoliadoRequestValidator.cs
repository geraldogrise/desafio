using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Consolidados.CreateConsolidado;

/// <summary>
/// Validator for CreateUserRequest that defines validation rules for consolidado creation.
/// </summary>
public class CreateConsolidadoRequestValidator : AbstractValidator<CreateConsolidadoRequest>
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
    public CreateConsolidadoRequestValidator()
    {
        RuleFor(consolidado => consolidado.DataConsolidado)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data do consolidado não pode ser futura.");

        RuleFor(consolidado => consolidado.ValorDebito)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor de débito não pode ser negativo.");

        RuleFor(consolidado => consolidado.ValorCredito)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor de crédito não pode ser negativo.");

        RuleFor(consolidado => consolidado.SaldoFinal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O saldo final não pode ser negativo.");

        RuleFor(consolidado => consolidado.CreatedAt)
            .NotEmpty()
            .WithMessage("A data de criação é obrigatória.");
    }
}