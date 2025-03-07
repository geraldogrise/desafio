using MediatR;
using Carrefour.Desafio.Common.Validation;

namespace Carrefour.Desafio.Application.Consolidados.CreateConsolidado;

/// <summary>
/// Command for creating a new consolidado.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a consolidado, 
/// including consolidadoname, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateConsolidadoResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateUserCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class CreateConsolidadoCommand : IRequest<CreateConsolidadoResult>
{
    public Guid Id { get; set; }
    public DateTime DataConsolidado { get; set; }
    public decimal ValorDebito { get; set; }
    public decimal ValorCredito { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ValidationResultDetail Validate()
    {
        var validator = new CreateConsolidadoCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}