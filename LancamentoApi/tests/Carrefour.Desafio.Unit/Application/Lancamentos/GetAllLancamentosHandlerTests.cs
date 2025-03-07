using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Carrefour.Desafio.Application.Lancamentos.DTOS;
using Carrefour.Desafio.Application.Lancamentos.GetAllLancamento;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Domain.Repositories;
using Carrefour.Desafio.Common.Result;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.Lancamentos;
public class GetAllLancamentosHandlerTests
{
    private readonly ILancamentoRepository _lancamentoRepository = Substitute.For<ILancamentoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetAllLancamentosHandler _handler;

    public GetAllLancamentosHandlerTests()
    {
        _handler = new GetAllLancamentosHandler(_lancamentoRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Return_Paginated_Lancamentos()
    {
        // Arrange
        var command = new GetAllLancamentosCommand { Page = 1, Size = 10, Order = "DataLancamento DESC" };
        var cancellationToken = new CancellationToken();

        // Criando a lista de Lancamento (entidade do domínio)
        var lancamentos = new List<Lancamento>
        {
            new Lancamento
            {
                Id = Guid.NewGuid(),
                Tipo = TipoLancamento.CREDITO,
                ValorLancamento = 100.50m,
                Descricao = "Pagamento recebido",
                Categoria = "Salário",
            },
            new Lancamento
            {
                Id = Guid.NewGuid(),
                Tipo = TipoLancamento.DEBITO,
                ValorLancamento = 50.75m,
                Descricao = "Compra de mercado",
                Categoria = "Alimentação",
            }
        };

        var pagedResult = new PagedResult<Lancamento>(lancamentos, lancamentos.Count, 1, 10);

        // Mockando a chamada ao repositório
        _lancamentoRepository.GetAllAsync(command.Page, command.Size, command.Order, cancellationToken)
            .Returns(Task.FromResult(pagedResult));

        // Criando a lista equivalente de DTOs
        var lancamentoDtos = new List<LancamentoDto>
        {
            new LancamentoDto
            {
                Id = lancamentos[0].Id,
                Tipo = lancamentos[0].Tipo,
                ValorLancamento = lancamentos[0].ValorLancamento,
                Descricao = lancamentos[0].Descricao,
                Categoria = lancamentos[0].Categoria,
            },
            new LancamentoDto
            {
                Id = lancamentos[1].Id,
                Tipo = lancamentos[1].Tipo,
                ValorLancamento = lancamentos[1].ValorLancamento,
                Descricao = lancamentos[1].Descricao,
                Categoria = lancamentos[1].Categoria,
            }
        };

        // Mockando o mapeamento de Lancamento -> LancamentoDto
        _mapper.Map<List<LancamentoDto>>(lancamentos).Returns(lancamentoDtos);

        // Mockando o mapeamento do resultado final
        _mapper.Map<GetAllLancamentosResult>(Arg.Any<GetAllLancamentosResult>())
            .Returns(new GetAllLancamentosResult
            {
                TotalItems = lancamentoDtos.Count,
                TotalPages = 1,
                Data = lancamentoDtos
            });

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.TotalItems.Should().Be(lancamentoDtos.Count);
        result.TotalPages.Should().Be(1);
        result.Data.Should().HaveCount(2);
        result.Data[0].Descricao.Should().Be("Pagamento recebido");
        result.Data[1].Categoria.Should().Be("Alimentação");
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var command = new GetAllLancamentosCommand { Page = 0, Size = 0, Order = "" }; // Parâmetros inválidos
        var cancellationToken = new CancellationToken();

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
