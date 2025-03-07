using Carrefour.Desafio.Application.Lancamentos.GetAllLancamento;
using AutoMapper;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetAllLancamento
{
    // <summary>
    /// Profile for mapping GetAllLancamentos feature requests to commands.
    /// </summary>
    public class GetAllLancamentosProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllLancamentos feature.
        /// </summary>
        public GetAllLancamentosProfile()
        {
            CreateMap<GetAllLancamentosRequest, GetAllLancamentosCommand>();
            CreateMap<GetAllLancamentosResult, GetAllLancamentosResponse>();

        }
    }
}
