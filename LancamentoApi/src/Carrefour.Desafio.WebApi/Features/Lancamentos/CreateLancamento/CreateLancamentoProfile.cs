using AutoMapper;
using Carrefour.Desafio.Application.Lancamentos.CreateLancamento;
namespace Carrefour.Desafio.WebApi.Features.Lancamentos.CreateLancamento;

/// <summary>
/// Profile for mapping between Application and API CreateLancamento responses
/// </summary>
public class CreateLancamentoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateLancamento feature
    /// </summary>
    public CreateLancamentoProfile()
    {
         CreateMap<CreateLancamentoRequest, CreateLancamentoCommand>();
        CreateMap<CreateLancamentoResult, CreateLancamentoResponse>();
    }
}
