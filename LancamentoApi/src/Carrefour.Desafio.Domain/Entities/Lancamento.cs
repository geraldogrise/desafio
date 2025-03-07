using Carrefour.Desafio.Common.Validation;
using Carrefour.Desafio.Domain.Common;
using Carrefour.Desafio.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
namespace Carrefour.Desafio.Domain.Entities
{
    public class Lancamento : BaseEntity
    {

        public DateTime DataLancamento { get; set; } = DateTime.UtcNow;

        public TipoLancamento Tipo { get; set; }

        public decimal ValorLancamento { get; set; }

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
