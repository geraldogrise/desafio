using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.CreateLancamento;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateLancamentoRequest
{
    public DateTime DataLancamento { get; set; } 

    public TipoLancamento Tipo { get; set; }

    public decimal ValorLancamento { get; set; }

    public string? Descricao { get; set; }

    public string? Categoria { get; set; }

    public DateTime CreatedAt { get; set; } 

    public DateTime UpdatedAt { get; set; } 

}