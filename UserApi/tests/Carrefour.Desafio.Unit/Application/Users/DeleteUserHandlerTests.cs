using Carrefour.Desafio.Application.Users.DeleteUser;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.UsersTest
{
    /// <summary>
    /// Contains unit tests for the <see cref="DeleteUserHandler"/> class.
    /// </summary>
    public class DeleteUserHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly DeleteUserHandler _handler;

        public DeleteUserHandlerTests()
        {
            _mapper = Substitute.For<IMapper>();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new DeleteUserHandler(_mapper, _userRepository);
        }

        /// <summary>
        /// Tests that a valid delete request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid user ID When deleting user Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new DeleteUserCommand(Guid.NewGuid());
            var user = new User { Id = command.Id, Username = "TestUser" };
            var expectedResult = new DeleteUserResult();

            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(user);
            _userRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(true);
            _mapper.Map<DeleteUserResult>(user).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            await _userRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that attempting to delete a non-existent user throws an exception.
        /// </summary>
        [Fact(DisplayName = "Given non-existent user ID When deleting user Then throws KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new DeleteUserCommand(Guid.NewGuid());
            _userRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((User)null);
            _userRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>()).Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        /// <summary>
        /// Tests that an invalid delete request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid user ID When deleting user Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new DeleteUserCommand(Guid.Empty);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
