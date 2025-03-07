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
public class GetConsolidadoByDateHandlerTests
{
    private readonly IConsolidadoRepository _consolidadoRepository = Substitute.For<IConsolidadoRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly GetConsoliadoByDateHandler _handler;

    public GetConsolidadoByDateHandlerTests()
    {
        _handler = new GetConsoliadoByDateHandler(_consolidadoRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_Return_Consolidado_Successfully()
    {
        // Arrange
        var date = DateTime.UtcNow.Date;
        var command = new GetConsolidadoByDateCommand(date);
        var cancellationToken = new CancellationToken();

        var consolidado = new Consolidado
        {
            Id = Guid.NewGuid(),
            DataConsolidado = date,
            ValorDebito = 100.00m,
            ValorCredito = 200.00m,
            SaldoFinal = 100.00m,
            CreatedAt = DateTime.UtcNow
        };

        var expectedResult = new GetConsolidadoByDateResult
        {
            Id = consolidado.Id,
            DataConsolidado = consolidado.DataConsolidado,
            ValorDebito = consolidado.ValorDebito,
            ValorCredito = consolidado.ValorCredito,
            SaldoFinal = consolidado.SaldoFinal
        };

        _consolidadoRepository.GetByDateAsync(date, cancellationToken).Returns(Task.FromResult(consolidado));
        _mapper.Map<GetConsolidadoByDateResult>(consolidado).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(consolidado.Id);
        result.DataConsolidado.Should().Be(date);
        result.ValorDebito.Should().Be(100.00m);
        result.ValorCredito.Should().Be(200.00m);
        result.SaldoFinal.Should().Be(100.00m);
    }

    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Consolidado_Not_Found()
    {
        // Arrange
        var date = DateTime.UtcNow.Date;
        var command = new GetConsolidadoByDateCommand(date);
        var cancellationToken = new CancellationToken();

        _consolidadoRepository.GetByDateAsync(date, cancellationToken).Returns(Task.FromResult<Consolidado?>(null));

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Consolidado with Date {date} not found");
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var command = new GetConsolidadoByDateCommand(default); // Data inválida
        var cancellationToken = new CancellationToken();

        // Act
        var act = async () => await _handler.Handle(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
