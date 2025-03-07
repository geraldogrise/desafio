using Carrefour.Desafio.Application.Users.GetUser;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.UsersTest
{
    public class GetUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly GetUserHandler _handler;

        public GetUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetUserHandler(_userRepository, _mapper);
        }

        [Fact(DisplayName = "Deve retornar os detalhes do usuário quando um usuário válido for encontrado")]
        public async Task Handle_ValidUserId_ReturnsUserDetails()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GetUserCommand(userId);
            var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };
            var expectedResult = new GetUserResult { Id = userId, Username = "testuser", Email = "test@example.com" };

            _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns(user);
            _mapper.Map<GetUserResult>(user).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.Username.Should().Be(user.Username);
            result.Email.Should().Be(user.Email);
        }

        [Fact(DisplayName = "Deve lançar KeyNotFoundException quando o usuário não for encontrado")]
        public async Task Handle_NonExistentUserId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = new GetUserCommand(userId);

            _userRepository.GetByIdAsync(userId, Arg.Any<CancellationToken>()).Returns((User)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {userId} not found");
        }

        [Fact(DisplayName = "Deve lançar ValidationException quando o ID for inválido")]
        public async Task Handle_InvalidUserId_ThrowsValidationException()
        {
            // Arrange
            var command = new GetUserCommand(Guid.Empty);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
