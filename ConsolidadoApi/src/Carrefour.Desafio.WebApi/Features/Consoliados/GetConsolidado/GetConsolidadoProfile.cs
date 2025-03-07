using AutoMapper;
using Carrefour.Desafio.Application.Consolidados.GetConsolidado;


namespace Carrefour.Desafio.WebApi.Features.Consolidados.GetConsolidado;

/// <summary>
/// Profile for mapping GetConsolidado feature requests to commands
/// </summary>
public class GetConsolidadoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetConsolidado feature
    /// </summary>
    public GetConsolidadoProfile()
    {
        CreateMap<Guid, Application.Consolidados.GetConsolidado.GetConsolidadoCommand>()
            .ConstructUsing(id => new Application.Consolidados.GetConsolidado.GetConsolidadoCommand(id));
        CreateMap<GetConsolidadoResult, GetConsolidadoResponse>().ReverseMap();
        

    }
}
