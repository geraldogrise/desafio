using AutoMapper;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Consolidados.GetConsolidado;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetConsolidadoByDateProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetConsolidadoByDateProfile()
    {
        CreateMap<Consolidado, GetConsolidadoByDateResult>();
    }
}
