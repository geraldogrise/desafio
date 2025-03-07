using FluentValidation;
using Carrefour.Desafio.Application.Consolidados.CreateConsolidado;

public class CreateConsolidadoCommandValidator : AbstractValidator<CreateConsolidadoCommand>
{
    public CreateConsolidadoCommandValidator()
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
