using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Carrefour.Desafio.Application.Consolidados.GetConsolidado;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.Consolidados;
public class GetConsolidadoHandlerTests
{
    private readonly IConsolidadoRepository _consolidadoRepository = Substitute.For<IConsolidadoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetConsolidadoHandler _handler;

    public GetConsolidadoHandlerTests()
    {
        _handler = new GetConsolidadoHandler(_consolidadoRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Return_Consolidado_Successfully()
    {
        // Arrange
        var consolidadoId = Guid.NewGuid();
        var command = new GetConsolidadoCommand(consolidadoId);
        var cancellationToken = new CancellationToken();

        var consolidado = new Consolidado
        {
            Id = consolidadoId,
            DataConsolidado = DateTime.UtcNow,
            ValorDebito = 200.00m,
            ValorCredito = 500.00m,
            SaldoFinal = 300.00m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var expectedResult = new GetConsolidadoResult
        {
            Id = consolidado.Id,
            DataConsolidado = consolidado.DataConsolidado,
            ValorDebito = consolidado.ValorDebito,
            ValorCredito = consolidado.ValorCredito,
            SaldoFinal = consolidado.SaldoFinal
        };

        _consolidadoRepository.GetByIdAsync(consolidadoId, cancellationToken).Returns(Task.FromResult(consolidado));
        _mapper.Map<GetConsolidadoResult>(consolidado).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(consolidadoId);
        result.ValorDebito.Should().Be(200.00m);
        result.ValorCredito.Should().Be(500.00m);
        result.SaldoFinal.Should().Be(300.00m);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Consolidado_Not_Found()
    {
        // Arrange
        var consolidadoId = Guid.NewGuid();
        var command = new GetConsolidadoCommand(consolidadoId);
        var cancellationToken = new CancellationToken();

        _consolidadoRepository.GetByIdAsync(consolidadoId, cancellationToken).Returns(Task.FromResult<Consolidado?>(null));

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Consolidado with ID {consolidadoId} not found");
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var command = new GetConsolidadoCommand(Guid.Empty); // ID inválido
        var cancellationToken = new CancellationToken();

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
