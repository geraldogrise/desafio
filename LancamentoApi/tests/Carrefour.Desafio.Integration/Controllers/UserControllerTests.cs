using System.Text;
using System.Text.Json;
using FluentAssertions;
using Xunit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;
using Carrefour.Desafio.Integration.Core;

namespace Carrefour.Desafio.Integration.Controllers
{
    public class UserControllerTests : IClassFixture<AmbevWebApplicationFactory>
    {
        private readonly HttpClient _client;
        // O mock do IMediator pode ser utilizado para verificar chamadas internas, mas
        // para testes de integração, é mais comum testar o fluxo completo.
        private readonly IMediator _mediatorMock;

        public UserControllerTests(AmbevWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _mediatorMock = factory.Services.GetRequiredService<IMediator>();
        }

        /// <summary>
        /// Testa a criação de um usuário (POST /api/users)
        /// </summary>
        [Fact]
        public async Task CreateUser_ShouldReturn201()
        {
            // Arrange: Utilize o DTO ou modelo de requisição adequado para criar o usuário.
            var request = new
            {
                Username = "João Silva",
                Email = "joao@email.com",
                Phone = "(11) 98765-4321",
                Password = "Senha@123",
                Role = UserRole.Admin.ToString(),   // Se a API espera string para Role
                Status = UserStatus.Active.ToString() // Se a API espera string para Status
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/users", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        /// <summary>
        /// Testa a busca de um usuário pelo ID (GET /api/users/{id})
        /// Para esse teste, cria-se um usuário primeiro e depois usa o seu ID para consulta.
        /// </summary>
        [Fact]
        public async Task GetUserById_ShouldReturn200_WhenExists()
        {
            // Arrange: Cria um usuário para garantir que ele exista.
            var createRequest = new
            {
                Username = "UserForGet",
                Email = "userforget@example.com",
                Phone = "(11) 12345-6789",
                Password = "Senha@123",
                Role = UserRole.Manager.ToString(),
                Status = UserStatus.Active.ToString()
            };

            var createContent = new StringContent(JsonSerializer.Serialize(createRequest), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/users", createContent);
            createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            // Recupera o ID do usuário criado (supondo que a resposta contenha o ID)
            var createdUserJson = await createResponse.Content.ReadAsStringAsync();
            // Aqui você pode desserializar o JSON para obter o ID, por exemplo:
            var createdUser = JsonSerializer.Deserialize<User>(createdUserJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            createdUser.Should().NotBeNull();

            // Act: Chama o endpoint para obter o usuário por ID
            var response = await _client.GetAsync($"/api/users/{createdUser.Id}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a busca de todos os usuários (GET /api/users)
        /// </summary>
        [Fact]
        public async Task GetAllUsers_ShouldReturn200()
        {
            // Act
            var response = await _client.GetAsync("/api/users");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a atualização de um usuário (PUT /api/users/{id})
        /// Para esse teste, cria-se um usuário, depois atualiza seus dados.
        /// </summary>
        [Fact]
        public async Task UpdateUser_ShouldReturn200_WhenValid()
        {
            // Arrange: Crie um usuário primeiro
            var createRequest = new
            {
                Username = "UserToUpdate",
                Email = "userupdate@example.com",
                Phone = "(11) 12345-6789",
                Password = "Senha@123",
                Role = UserRole.Manager.ToString(),
                Status = UserStatus.Active.ToString()
            };

            var createContent = new StringContent(JsonSerializer.Serialize(createRequest), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/users", createContent);
            createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            var createdUserJson = await createResponse.Content.ReadAsStringAsync();
            var createdUser = JsonSerializer.Deserialize<User>(createdUserJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            createdUser.Should().NotBeNull();

            // Prepara os dados para atualização
            var updateRequest = new
            {
                Username = "UserUpdated",
                Email = "updated@example.com",
                Phone = "(11) 99999-9999",
                Password = "NovaSenha@456",
                Role = UserRole.Admin.ToString(),
                Status = UserStatus.Inactive.ToString()
            };

            var updateContent = new StringContent(JsonSerializer.Serialize(updateRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/users/{createdUser.Id}", updateContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        /// Testa a exclusão de um usuário (DELETE /api/users/{id})
        /// Para esse teste, cria-se um usuário e, em seguida, o exclui.
        /// </summary>
        [Fact]
        public async Task DeleteUser_ShouldReturn200_WhenExists()
        {
            // Arrange: Cria um usuário para exclusão
            var createRequest = new
            {
                Username = "UserToDelete",
                Email = "usertodelete@example.com",
                Phone = "(11) 12345-6789",
                Password = "Senha@123",
                Role = UserRole.Manager.ToString(),
                Status = UserStatus.Active.ToString()
            };

            var createContent = new StringContent(JsonSerializer.Serialize(createRequest), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/users", createContent);
            createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            var createdUserJson = await createResponse.Content.ReadAsStringAsync();
            var createdUser = JsonSerializer.Deserialize<User>(createdUserJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            createdUser.Should().NotBeNull();

            // Act: Exclui o usuário
            var response = await _client.DeleteAsync($"/api/users/{createdUser.Id}");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
