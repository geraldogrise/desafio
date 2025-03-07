using AutoMapper;
using Carrefour.Desafio.Application.Consolidados.CreateConsolidado;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.Consolidados
{
    public class CreateConsolidadoHandlerTests
    {
        private readonly IConsolidadoRepository _consolidadoRepository = Substitute.For<IConsolidadoRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly CreateConsolidadoHandler _handler;

        public CreateConsolidadoHandlerTests()
        {
            _handler = new CreateConsolidadoHandler(_consolidadoRepository, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Create_Consolidado_Successfully()
        {
            // Arrange
            var command = new CreateConsolidadoCommand
            {
                Id = Guid.NewGuid(),
                DataConsolidado = DateTime.UtcNow,
                ValorDebito = 100,
                ValorCredito = 150,
                SaldoFinal = 50,
                CreatedAt = DateTime.UtcNow
            };
            var cancellationToken = new CancellationToken();

            var consolidadoEntity = new Consolidado
            {
                Id = command.Id,
                DataConsolidado = command.DataConsolidado,
                ValorDebito = command.ValorDebito,
                ValorCredito = command.ValorCredito,
                SaldoFinal = command.SaldoFinal,
                CreatedAt = command.CreatedAt
            };

            var result = new CreateConsolidadoResult
            {
                Id = consolidadoEntity.Id,
                DataConsolidado = consolidadoEntity.DataConsolidado,
                ValorDebito = consolidadoEntity.ValorDebito,
                ValorCredito = consolidadoEntity.ValorCredito,
                SaldoFinal = consolidadoEntity.SaldoFinal,
                CreatedAt = consolidadoEntity.CreatedAt
            };

            _consolidadoRepository.GetByDateAsync(command.DataConsolidado)
                .Returns((Consolidado)null); // No existing consolidado for this date

            _consolidadoRepository.CreateAsync(Arg.Any<Consolidado>(), cancellationToken)
                .Returns(Task.FromResult(consolidadoEntity));

            _mapper.Map<CreateConsolidadoResult>(consolidadoEntity).Returns(result);

            // Act
            var actualResult = await _handler.Handle(command, cancellationToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Id.Should().Be(command.Id);
            actualResult.DataConsolidado.Should().Be(command.DataConsolidado);
            actualResult.ValorDebito.Should().Be(command.ValorDebito);
            actualResult.ValorCredito.Should().Be(command.ValorCredito);
            actualResult.SaldoFinal.Should().Be(command.SaldoFinal);
            actualResult.CreatedAt.Should().Be(command.CreatedAt);
        }

        [Fact]
        public async Task Handle_Should_Update_Consolidado_When_Exists()
        {
            // Arrange
            var command = new CreateConsolidadoCommand
            {
                Id = Guid.NewGuid(),
                DataConsolidado = DateTime.UtcNow,
                ValorDebito = 100,
                ValorCredito = 150,
                SaldoFinal = 50,
                CreatedAt = DateTime.UtcNow
            };
            var cancellationToken = new CancellationToken();

            var existingConsolidado = new Consolidado
            {
                Id = command.Id,
                DataConsolidado = command.DataConsolidado,
                ValorDebito = 80,
                ValorCredito = 120,
                SaldoFinal = 40,
                CreatedAt = DateTime.UtcNow
            };

            var updatedConsolidado = new Consolidado
            {
                Id = command.Id,
                DataConsolidado = command.DataConsolidado,
                ValorDebito = command.ValorDebito,
                ValorCredito = command.ValorCredito,
                SaldoFinal = command.SaldoFinal,
                CreatedAt = command.CreatedAt
            };

            var result = new CreateConsolidadoResult
            {
                Id = updatedConsolidado.Id,
                DataConsolidado = updatedConsolidado.DataConsolidado,
                ValorDebito = updatedConsolidado.ValorDebito,
                ValorCredito = updatedConsolidado.ValorCredito,
                SaldoFinal = updatedConsolidado.SaldoFinal,
                CreatedAt = updatedConsolidado.CreatedAt
            };

            _consolidadoRepository.GetByDateAsync(command.DataConsolidado)
                .Returns(existingConsolidado); // Existing consolidado for this date

            _consolidadoRepository.UpdateAsync(existingConsolidado.Id, Arg.Any<Consolidado>(), cancellationToken)
                .Returns(Task.FromResult(updatedConsolidado));

            _mapper.Map<CreateConsolidadoResult>(updatedConsolidado).Returns(result);

            // Act
            var actualResult = await _handler.Handle(command, cancellationToken);

            // Assert
            actualResult.Should().NotBeNull();
            actualResult.Id.Should().Be(command.Id);
            actualResult.DataConsolidado.Should().Be(command.DataConsolidado);
            actualResult.ValorDebito.Should().Be(command.ValorDebito);
            actualResult.ValorCredito.Should().Be(command.ValorCredito);
            actualResult.SaldoFinal.Should().Be(command.SaldoFinal);
            actualResult.CreatedAt.Should().Be(command.CreatedAt);
        }

        [Fact]
        public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
        {
            // Arrange
            var invalidCommand = new CreateConsolidadoCommand
            {
                Id = Guid.Empty, // Invalid Id
                DataConsolidado = DateTime.UtcNow,
                ValorDebito = 0,
                ValorCredito = -1,
                SaldoFinal = -1,
                CreatedAt = DateTime.UtcNow
            };
            var cancellationToken = new CancellationToken();

            var validator = new CreateConsolidadoCommandValidator();
            var validationResult = await validator.ValidateAsync(invalidCommand, cancellationToken);

            // Act
            var act = async () => await _handler.Handle(invalidCommand, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
