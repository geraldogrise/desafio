using Carrefour.Desafio.Application.Users.CreateUser;
using Carrefour.Desafio.Application.Users.DeleteUser;
using Carrefour.Desafio.Domain.Entities;
using AutoMapper;

namespace Carrefour.Desafio.WebApi.Features.Users.DeleteUser;

/// <summary>
/// Profile for mapping DeleteUser feature requests to commands
/// </summary>
public class DeleteUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteUser feature
    /// </summary>
    public DeleteUserProfile()
    {
        CreateMap<Guid, Application.Users.DeleteUser.DeleteUserCommand>()
            .ConstructUsing(id => new Application.Users.DeleteUser.DeleteUserCommand(id));
        CreateMap<DeleteUserResult, DeleteUserResponse>().ReverseMap();
        CreateMap<User, DeleteUserResult>();


    }
}
