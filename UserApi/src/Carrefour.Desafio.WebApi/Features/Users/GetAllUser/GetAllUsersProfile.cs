using Carrefour.Desafio.Application.Users.GetAllUser;
using AutoMapper;

namespace Carrefour.Desafio.WebApi.Features.Users.GetAllUser
{
    // <summary>
    /// Profile for mapping GetAllUsers feature requests to commands.
    /// </summary>
    public class GetAllUsersProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllUsers feature.
        /// </summary>
        public GetAllUsersProfile()
        {
            CreateMap<GetAllUsersRequest, GetAllUsersCommand>();
            CreateMap<GetAllUsersResult, GetAllUsersResponse>();

        }
    }
}
