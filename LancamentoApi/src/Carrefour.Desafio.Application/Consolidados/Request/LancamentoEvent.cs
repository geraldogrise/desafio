using Carrefour.Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Consolidados.Request
{
    public class LancamentoEvent
    {
        public string EventId { get; set; }
        public Guid Id { get; set; }
        public DateTime DataConsolidado { get; set; }
        public decimal ValorDebito { get; set; }
        public decimal ValorCredito { get; set; }
        public string Token { get; set; }
    }
}
