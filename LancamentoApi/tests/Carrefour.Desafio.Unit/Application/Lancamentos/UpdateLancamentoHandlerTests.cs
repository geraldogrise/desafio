using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Carrefour.Desafio.Application.Lancamentos.UpdateLancamento;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.Lancamentos;
public class UpdateLancamentoHandlerTests
{
    private readonly ILancamentoRepository _lancamentoRepository = Substitute.For<ILancamentoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly UpdateLamcamentoHandler _handler;

    public UpdateLancamentoHandlerTests()
    {
        _handler = new UpdateLamcamentoHandler(_lancamentoRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Update_Lancamento_Successfully()
    {
        // Arrange
        var lancamentoId = Guid.NewGuid();
        var command = new UpdateLancamentoCommand
        {
            Id = lancamentoId,
            Tipo = TipoLancamento.DEBITO,
            ValorLancamento = 250.50m,
            Descricao = "Pagamento de conta",
            Categoria = "Despesas",
            UpdatedAt = DateTime.UtcNow
        };

        var lancamento = new Lancamento
        {
            Id = lancamentoId,
            Tipo = TipoLancamento.CREDITO, // Antes da atualização
            ValorLancamento = 100.00m,
            Descricao = "Recebimento",
            Categoria = "Salário",
            UpdatedAt = DateTime.UtcNow
        };

        _lancamentoRepository.GetByIdAsync(lancamentoId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(lancamento));

        _mapper.Map(command, lancamento);

        _lancamentoRepository.UpdateAsync(lancamentoId, lancamento, Arg.Any<CancellationToken>())
         .Returns(Task.FromResult<Lancamento?>(lancamento));

        var expectedResult = new UpdateLancamentoResult
        {
            Id = lancamentoId,
            DataLancamento = lancamento.DataLancamento,
            Tipo = command.Tipo,
            ValorLancamento = command.ValorLancamento,
            Descricao = command.Descricao,
            Categoria = command.Categoria,
            CreatedAt = lancamento.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        _mapper.Map<UpdateLancamentoResult>(lancamento).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.ValorLancamento.Should().Be(250.50m);
        result.Descricao.Should().Be("Pagamento de conta");
        _lancamentoRepository.UpdateAsync(lancamentoId, lancamento, Arg.Any<CancellationToken>())
        .Returns(Task.FromResult<Lancamento?>(lancamento));
    }

    [Fact]
    public async Task Handle_Should_Throw_InvalidOperationException_When_Lancamento_Not_Found()
    {
        // Arrange
        var lancamentoId = Guid.NewGuid();
        var command = new UpdateLancamentoCommand
        {
            Id = lancamentoId,
            Tipo = TipoLancamento.DEBITO,
            ValorLancamento = 100m,
            Descricao = "Teste",
            Categoria = "Teste",
            UpdatedAt = DateTime.UtcNow
        };

        _lancamentoRepository.GetByIdAsync(lancamentoId, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Lancamento?>(null)); // Simula "não encontrado"

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Lancamento with ID {command.Id} not found");
    }


    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var command = new UpdateLancamentoCommand { Id = Guid.Empty }; // ID inválido

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
