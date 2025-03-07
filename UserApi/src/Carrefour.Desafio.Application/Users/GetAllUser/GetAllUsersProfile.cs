using AutoMapper;
using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Common.Result;
using Carrefour.Desafio.Application.Users.DTOS;

namespace Carrefour.Desafio.Application.Users.GetAllUser
{
    /// <summary>
    /// Profile for mapping between User entity and GetAllUsersResult
    /// </summary>
    public class GetAllUsersProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllUsers operation
        /// </summary>
        public GetAllUsersProfile()
        {
            CreateMap<PagedResult<User>, GetAllUsersResult>();
            CreateMap<User, UserDto>();
        }
    }
}
