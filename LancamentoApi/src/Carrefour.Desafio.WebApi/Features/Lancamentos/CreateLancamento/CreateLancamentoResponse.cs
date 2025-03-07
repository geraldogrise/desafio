using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.CreateLancamento;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class CreateLancamentoResponse
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
