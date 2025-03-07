using AutoMapper;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Common.Result;
using Carrefour.Desafio.Application.Lancamentos.DTOS;

namespace Carrefour.Desafio.Application.Lancamentos.GetAllLancamento
{
    /// <summary>
    /// Profile for mapping between User entity and GetAllLancamentosResult
    /// </summary>
    public class GetAllULancamentosProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllLancamentos operation
        /// </summary>
        public GetAllULancamentosProfile()
        {
            CreateMap<PagedResult<Lancamento>, GetAllLancamentosResult>();
            CreateMap<Lancamento, LancamentoDto>();
        }
    }
}
