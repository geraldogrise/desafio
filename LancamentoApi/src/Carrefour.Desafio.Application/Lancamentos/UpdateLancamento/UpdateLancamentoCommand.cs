using MediatR;
using Carrefour.Desafio.Common.Validation;
using Carrefour.Desafio.Domain.Enums;


namespace Carrefour.Desafio.Application.Lancamentos.UpdateLancamento
{
    /// <summary>
    /// Command for updating an existing user.
    /// </summary>
    public class UpdateLancamentoCommand : IRequest<UpdateLancamentoResult>
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime DataLancamento { get; set; } = DateTime.UtcNow;

        public TipoLancamento Tipo { get; set; }

        public decimal ValorLancamento { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Validates the command.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateLancamentoCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
