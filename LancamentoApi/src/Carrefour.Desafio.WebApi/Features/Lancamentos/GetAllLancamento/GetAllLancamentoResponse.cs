using Carrefour.Desafio.Application.Lancamentos.DTOS;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetAllLancamento
{
    /// <summary>
    /// Response model for retrieving all users.
    /// </summary>
    public class GetAllLancamentosResponse
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
