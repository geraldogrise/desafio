using Carrefour.Desafio.Application.Consolidados.GetAllConsolidado;
using AutoMapper;

namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetAllConsolidado
{
    // <summary>
    /// Profile for mapping GetAllConsolidados feature requests to commands.
    /// </summary>
    public class GetAllConsolidadosProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllConsolidados feature.
        /// </summary>
        public GetAllConsolidadosProfile()
        {
            CreateMap<GetAllConsolidadosRequest, GetAllConsolidadosCommand>();
            CreateMap<GetAllConsolidadosResult, GetAllConsolidadosResponse>();

        }
    }
}
