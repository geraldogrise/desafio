using Carrefour.Desafio.Application.Lancamentos.DTOS;
using Carrefour.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Lancamentos.DeleteLancamento
{
    public class DeleteLancamentoResult
    {
        /// <summary>
        /// Indicates whether the deletion was successful
        /// </summary>
        public bool Success { get; set; }

        public Guid Id { get; set; } 

        public DateTime DataLancamento { get; set; } 

        public TipoLancamento Tipo { get; set; }

        public decimal ValorLancamento { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
