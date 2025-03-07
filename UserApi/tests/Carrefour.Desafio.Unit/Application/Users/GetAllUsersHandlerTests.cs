using Carrefour.Desafio.Application.Users.DTOS;
using Carrefour.Desafio.Application.Users.GetAllUser;
using Carrefour.Desafio.Common.Result;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Carrefour.Desafio.Unit.Application.UsersTest
{
    public class GetAllUsersHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly GetAllUsersHandler _handler;

        public GetAllUsersHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllUsersHandler(_userRepository, _mapper);
        }

        [Fact(DisplayName = "Deve retornar usuários paginados corretamente")]
        public async Task Handle_ValidRequest_ReturnsPagedUsers()
        {
            // Arrange
            var request = new GetAllUsersCommand { Page = 1, Size = 10, Order = "asc" };
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "user1", Email = "user1@example.com" },
                new User { Id = Guid.NewGuid(), Username = "user2", Email = "user2@example.com" }
            };

            var pagedResult = new PagedResult<User>(users, totalItems: 2, totalPages: 1);
            var expectedDtos = users.Select(u => new UserDto { Id = u.Id, Username = u.Username, Email = u.Email }).ToList();
            var expectedResult = new GetAllUsersResult { TotalItems = 2, TotalPages = 1, Data = expectedDtos };

            _userRepository.GetAllAsync(request.Page, request.Size, request.Order, Arg.Any<CancellationToken>())
                .Returns(pagedResult);

            _mapper.Map<List<UserDto>>(users).Returns(expectedDtos);
            _mapper.Map<GetAllUsersResult>(Arg.Any<GetAllUsersResult>()).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.TotalItems.Should().Be(2);
            result.TotalPages.Should().Be(1);
            result.Data.Should().HaveCount(2);
            result.Data.First().Username.Should().Be("user1");
            result.Data.Last().Username.Should().Be("user2");
        }

        [Fact(DisplayName = "Deve retornar lista vazia quando nenhum usuário for encontrado")]
        public async Task Handle_NoUsersFound_ReturnsEmptyList()
        {
            // Arrange
            var request = new GetAllUsersCommand { Page = 1, Size = 10, Order = "asc" };
            var pagedResult = new PagedResult<User>(new List<User>(), totalItems: 0, totalPages: 0);

            _userRepository.GetAllAsync(request.Page, request.Size, request.Order, Arg.Any<CancellationToken>())
                .Returns(pagedResult);

            _mapper.Map<List<UserDto>>(Arg.Any<List<User>>()).Returns(new List<UserDto>());
            _mapper.Map<GetAllUsersResult>(Arg.Any<GetAllUsersResult>()).Returns(new GetAllUsersResult
            {
                TotalItems = 0,
                TotalPages = 0,
                Data = new List<UserDto>()
            });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.TotalItems.Should().Be(0);
            result.TotalPages.Should().Be(0);
            result.Data.Should().BeEmpty();
        }

        [Fact(DisplayName = "Deve lançar ValidationException quando os parâmetros forem inválidos")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var request = new GetAllUsersCommand { Page = 0, Size = -1, Order = "invalid" }; // Valores inválidos

            // Act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
