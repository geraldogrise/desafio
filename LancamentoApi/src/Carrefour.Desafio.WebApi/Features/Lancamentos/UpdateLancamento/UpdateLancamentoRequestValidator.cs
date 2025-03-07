using Carrefour.Desafio.Domain.Enums;
using FluentValidation;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.UpdateLancamento
{
    // <summary>
    /// Validator for UpdateUserRequest that defines validation rules for user update.
    /// </summary>
    public class UpdateLancamentoRequestValidator : AbstractValidator<UpdateLancamentoRequest>
    {
        /// <summary>
        /// Initializes a new instance of the UpdateUserRequestValidator with defined validation rules.
        /// </summary>
        public UpdateLancamentoRequestValidator()
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
}
