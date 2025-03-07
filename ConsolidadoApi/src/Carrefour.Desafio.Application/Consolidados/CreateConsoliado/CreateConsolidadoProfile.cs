using AutoMapper;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Consolidados.CreateConsolidado;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateConsolidadoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateConsolidadoProfile()
    {
        CreateMap<CreateConsolidadoCommand, Consolidado>();
        CreateMap<Consolidado, CreateConsolidadoResult>();
    }
}
