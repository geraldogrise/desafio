using AutoMapper;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Lancamentos.UpdateLancamento
{

    /// <summary>
    /// Profile for mapping between User entity and UpdateUserResponse.
    /// </summary>
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<UpdateLancamentoCommand, Lancamento>();
            CreateMap<Lancamento, UpdateLancamentoResult>();
        }
    }
}
