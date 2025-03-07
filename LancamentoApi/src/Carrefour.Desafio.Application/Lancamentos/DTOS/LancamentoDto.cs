using Carrefour.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Lancamentos.DTOS
{

    /// <summary>
    /// Represents a user in the GetAllLancamentos response
    /// </summary>
    public class LancamentoDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime DataLancamento { get; set; } = DateTime.UtcNow;

        public TipoLancamento Tipo { get; set; }

        public decimal ValorLancamento { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public String Token { get; set; } = string.Empty;

    }
}
