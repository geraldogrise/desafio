using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetLancamento;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class GetLancamentoResponse
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
