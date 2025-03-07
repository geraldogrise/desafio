using AutoMapper;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Lancamentos.CreateLancamento;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateLancamentoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateLancamentoProfile()
    {
        CreateMap<CreateLancamentoCommand, Lancamento>();
        CreateMap<Lancamento, CreateLancamentoResult>();
    }
}
