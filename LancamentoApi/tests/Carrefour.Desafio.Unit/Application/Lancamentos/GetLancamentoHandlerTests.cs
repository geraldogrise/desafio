using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Carrefour.Desafio.Application.Lancamentos.GetLancamento;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.Lancamentos;
public class GetLancamentoHandlerTests
{
    private readonly ILancamentoRepository _lancamentoRepository = Substitute.For<ILancamentoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetLancamentoHandler _handler;

    public GetLancamentoHandlerTests()
    {
        _handler = new GetLancamentoHandler(_lancamentoRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Return_Lancamento_Successfully()
    {
        // Arrange
        var lancamentoId = Guid.NewGuid();
        var command = new GetLancamentoCommand(lancamentoId);
        var cancellationToken = new CancellationToken();

        var lancamento = new Lancamento
        {
            Id = lancamentoId,
            Tipo = TipoLancamento.CREDITO,
            ValorLancamento = 150.75m,
            Descricao = "Recebimento de bônus",
            Categoria = "Salário",
        };

        var expectedResult = new GetLancamentoResult
        {
            Id = lancamento.Id,
            Tipo = lancamento.Tipo,
            ValorLancamento = lancamento.ValorLancamento,
            Descricao = lancamento.Descricao,
            Categoria = lancamento.Categoria,
        };

        _lancamentoRepository.GetByIdAsync(lancamentoId, cancellationToken).Returns(Task.FromResult(lancamento));
        _mapper.Map<GetLancamentoResult>(lancamento).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(lancamentoId);
        result.Descricao.Should().Be("Recebimento de bônus");
        result.ValorLancamento.Should().Be(150.75m);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Lancamento_Not_Found()
    {
        // Arrange
        var lancamentoId = Guid.NewGuid();
        var command = new GetLancamentoCommand(lancamentoId);
        var cancellationToken = new CancellationToken();

        _lancamentoRepository.GetByIdAsync(lancamentoId, cancellationToken).Returns(Task.FromResult<Lancamento?>(null));

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Lancamento with ID {lancamentoId} not found");
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var command = new GetLancamentoCommand(Guid.Empty); // ID inválido
        var cancellationToken = new CancellationToken();

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
