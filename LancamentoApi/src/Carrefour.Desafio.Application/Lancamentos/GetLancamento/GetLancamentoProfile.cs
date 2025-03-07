using AutoMapper;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Lancamentos.GetLancamento;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetLancamentoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetLancamentoProfile()
    {
        CreateMap<Lancamento, GetLancamentoResult>();
    }
}
