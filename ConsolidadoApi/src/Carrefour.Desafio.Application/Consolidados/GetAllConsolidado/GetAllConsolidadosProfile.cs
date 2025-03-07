using AutoMapper;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Common.Result;
using Carrefour.Desafio.Application.Consolidados.DTOS;

namespace Carrefour.Desafio.Application.Consolidados.GetAllConsolidado
{
    /// <summary>
    /// Profile for mapping between User entity and GetAllConsolidadosResult
    /// </summary>
    public class GetAllUConsolidadosProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllConsolidados operation
        /// </summary>
        public GetAllUConsolidadosProfile()
        {
            CreateMap<PagedResult<Consolidado>, GetAllConsolidadosResult>();
            CreateMap<Consolidado, ConsolidadoDto>();
        }
    }
}
