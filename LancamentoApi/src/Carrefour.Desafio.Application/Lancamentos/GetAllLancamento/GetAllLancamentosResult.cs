using Carrefour.Desafio.Application.Lancamentos.DTOS;
using Carrefour.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Lancamentos.GetAllLancamento
{

    /// <summary>
    /// Response model for GetAllLancamentos operation
    /// </summary>
    public class GetAllLancamentosResult
    {
        /// <summary>
        /// The total number of users
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The list of users
        /// </summary>
        public List<LancamentoDto> Data { get; set; } = new();

    }
}
