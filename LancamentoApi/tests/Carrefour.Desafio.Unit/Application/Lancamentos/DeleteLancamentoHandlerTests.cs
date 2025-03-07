using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Carrefour.Desafio.Application.Lancamentos.DeleteLancamento;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.Lancamentos;
public class DeleteLancamentoHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly DeleteLancamentoHandler _handler;

    public DeleteLancamentoHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _lancamentoRepository = Substitute.For<ILancamentoRepository>();
        _handler = new DeleteLancamentoHandler(_mapper, _lancamentoRepository);
    }

    [Fact]
    public async Task Handle_Should_Delete_Lancamento_When_Exists()
    {
        // Arrange
        var lancamentoId = Guid.NewGuid();
        var command = new DeleteLancamentoCommand(lancamentoId);
        var cancellationToken = new CancellationToken();
        var lancamento = new Lancamento { Id = lancamentoId };

        _lancamentoRepository.GetByIdAsync(lancamentoId, cancellationToken).Returns(Task.FromResult(lancamento));
        _lancamentoRepository.DeleteAsync(lancamentoId, cancellationToken).Returns(Task.FromResult(true));
        _mapper.Map<DeleteLancamentoResult>(lancamento).Returns(new DeleteLancamentoResult());

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        await _lancamentoRepository.Received(1).DeleteAsync(lancamentoId, cancellationToken);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_When_Lancamento_Not_Found()
    {
        // Arrange
        var lancamentoId = Guid.NewGuid();
        var command = new DeleteLancamentoCommand(lancamentoId);
        var cancellationToken = new CancellationToken();

        _lancamentoRepository.GetByIdAsync(lancamentoId, cancellationToken).Returns(Task.FromResult<Lancamento>(null));

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Lancamentos with ID {lancamentoId} not found");
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Invalid_Request()
    {
        // Arrange
        var command = new DeleteLancamentoCommand(Guid.Empty);
        var cancellationToken = new CancellationToken();

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
