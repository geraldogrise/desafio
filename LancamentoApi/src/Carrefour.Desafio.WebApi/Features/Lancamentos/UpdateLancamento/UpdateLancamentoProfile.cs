using Carrefour.Desafio.Application.Lancamentos.UpdateLancamento;
using AutoMapper;

namespace Carrefour.Desafio.WebApi.Features.Lancamentos.UpdateLancamento
{
    /// <summary>
    /// Profile for mapping between Application and API UpdateUser responses
    /// </summary>
    public class UpdateLancamentoProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateUser feature
        /// </summary>
        public UpdateLancamentoProfile()
        {
            CreateMap<UpdateLancamentoRequest, UpdateLancamentoCommand>();
            CreateMap<UpdateLancamentoResult, UpdateLancamentoResponse>();
        }
    }
}
