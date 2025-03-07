using AutoMapper;
using Carrefour.Desafio.Application.Lancamentos.GetLancamento;


namespace Carrefour.Desafio.WebApi.Features.Lancamentos.GetLancamento;

/// <summary>
/// Profile for mapping GetLancamento feature requests to commands
/// </summary>
public class GetLancamentoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetLancamento feature
    /// </summary>
    public GetLancamentoProfile()
    {
        CreateMap<Guid, Application.Lancamentos.GetLancamento.GetLancamentoCommand>()
            .ConstructUsing(id => new Application.Lancamentos.GetLancamento.GetLancamentoCommand(id));
        CreateMap<GetLancamentoResult, GetLancamentoResponse>().ReverseMap();
        

    }
}
