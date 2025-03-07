using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.Application.Lancamentos.CreateLancamento;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateLancamentoResult
{
    public Guid Id { get; set; } 

    public DateTime DataLancamento { get; set; } 

    public TipoLancamento Tipo { get; set; }

    public decimal ValorLancamento { get; set; }

    public string? Descricao { get; set; }

    public string? Categoria { get; set; }

    public DateTime CreatedAt { get; set; } 

    public DateTime UpdatedAt { get; set; } 

}
