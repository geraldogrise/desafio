using Carrefour.Desafio.Application.Lancamentos.DTOS;
using Carrefour.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Lancamentos.UpdateLancamento
{

    /// <summary>
    /// Represents the response returned after successfully updating a user.
    /// </summary>
    public class UpdateLancamentoResult
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
}
