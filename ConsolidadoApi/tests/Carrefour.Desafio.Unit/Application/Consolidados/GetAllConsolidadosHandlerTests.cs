using AutoMapper;
using Carrefour.Desafio.Application.Consolidados.DTOS;
using Carrefour.Desafio.Application.Consolidados.GetAllConsolidado;
using Carrefour.Desafio.Common.Result;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace Carrefour.Desafio.Unit.Application.Consolidados
{
    public class GetAllConsolidadosHandlerTests
    {
        private readonly IConsolidadoRepository _consolidadoRepository = Substitute.For<IConsolidadoRepository>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly GetAllConsolidadosHandler _handler;

        public GetAllConsolidadosHandlerTests()
        {
            _handler = new GetAllConsolidadosHandler(_consolidadoRepository, _mapper);
        }

      

        [Fact]
        public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
        {
            // Arrange
            var command = new GetAllConsolidadosCommand { Page = 0, Size = 0, Order = "" }; // Invalid values
            var cancellationToken = new CancellationToken();

            // Act
            var act = async () => await _handler.Handle(command, cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
