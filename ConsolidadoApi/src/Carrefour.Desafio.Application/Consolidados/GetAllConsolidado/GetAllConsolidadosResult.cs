using Carrefour.Desafio.Application.Consolidados.DTOS;

namespace Carrefour.Desafio.Application.Consolidados.GetAllConsolidado
{

    /// <summary>
    /// Response model for GetAllConsolidados operation
    /// </summary>
    public class GetAllConsolidadosResult
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
