using AutoMapper;
using Carrefour.Desafio.Application.Consolidados.GetConsolidado;


namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;

/// <summary>
/// Profile for mapping GetConsolidado feature requests to commands
/// </summary>
public class GetConsolidadoByDateProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetConsolidado feature
    /// </summary>
    public GetConsolidadoByDateProfile()
    {
        CreateMap<DateTime, Application.Consolidados.GetConsolidado.GetConsolidadoByDateCommand>()
            .ConstructUsing(date => new Application.Consolidados.GetConsolidado.GetConsolidadoByDateCommand(date));
        CreateMap<GetConsolidadoByDateResult, GetConsolidadoByDateResponse>().ReverseMap();
        

    }
}
