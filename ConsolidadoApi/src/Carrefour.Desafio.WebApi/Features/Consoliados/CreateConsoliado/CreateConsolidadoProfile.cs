using AutoMapper;
using Carrefour.Desafio.Application.Consolidados.CreateConsolidado;
namespace Carrefour.Desafio.WebApi.Features.Consolidados.CreateConsolidado;

/// <summary>
/// Profile for mapping between Application and API CreateConsolidado responses
/// </summary>
public class CreateConsolidadoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateConsolidado feature
    /// </summary>
    public CreateConsolidadoProfile()
    {
         CreateMap<CreateConsolidadoRequest, CreateConsolidadoCommand>();
        CreateMap<CreateConsolidadoResult, CreateConsolidadoResponse>();
    }
}
