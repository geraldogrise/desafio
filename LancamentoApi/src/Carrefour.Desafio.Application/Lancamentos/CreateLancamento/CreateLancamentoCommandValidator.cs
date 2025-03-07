using FluentValidation;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Application.Lancamentos.CreateLancamento;

public class CreateLancamentoCommandValidator : AbstractValidator<CreateLancamentoCommand>
{
    public CreateLancamentoCommandValidator()
    {
        RuleFor(lancamento => lancamento.DataLancamento)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data do lançamento não pode ser futura.");

        RuleFor(lancamento => lancamento.Tipo)
            .NotEmpty()
            .Must(tipo => tipo == TipoLancamento.DEBITO|| tipo == TipoLancamento.CREDITO)
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
