using Carrefour.Desafio.Application.Consolidados.DTOS;

namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetAllConsolidado
{
    /// <summary>
    /// Response model for retrieving all consolidados.
    /// </summary>
    public class GetAllConsolidadosResponse
    {
        /// <summary>
        /// The total number of consolidados
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The list of consolidados
        /// </summary>
        public List<ConsolidadoDto> Data { get; set; } = new();
    }
}
