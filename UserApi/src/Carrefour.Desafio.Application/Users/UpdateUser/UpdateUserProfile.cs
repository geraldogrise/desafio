using AutoMapper;
using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Application.Users.UpdateUser
{

    /// <summary>
    /// Profile for mapping between User entity and UpdateUserResponse.
    /// </summary>
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UpdateUserResult>();
        }
    }
}
