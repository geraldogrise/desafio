using AutoMapper;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Application.Lancamentos.DeleteLancamento;
using Carrefour.Desafio.WebApi.Features.Lancamentos.DeleteLancamento;


namespace Carrefour.Desafio.WebApi.Features.Lancamentos.Deletelancamento;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteLancamentoProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteUser feature
    /// </summary>
    public DeleteLancamentoProfile()
    {
        CreateMap<Guid, Application.Lancamentos.DeleteLancamento.DeleteLancamentoCommand>()
            .ConstructUsing(id => new Application.Lancamentos.DeleteLancamento.DeleteLancamentoCommand(id));
        CreateMap<DeleteLancamentoResult, DeleteLancamentoResponse>().ReverseMap();
        CreateMap<Lancamento, DeleteLancamentoResult>();


    }
}
