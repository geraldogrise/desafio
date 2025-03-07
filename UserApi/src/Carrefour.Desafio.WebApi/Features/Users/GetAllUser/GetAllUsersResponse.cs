using Carrefour.Desafio.Application.Users.DTOS;
using static Carrefour.Desafio.Application.Users.GetAllUser.GetAllUsersResult;

namespace Carrefour.Desafio.WebApi.Features.Users.GetAllUser
{
    /// <summary>
    /// Response model for retrieving all users.
    /// </summary>
    public class GetAllUsersResponse
    {
        /// <summary>
        /// The total number of users
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The list of users
        /// </summary>
        public List<UserDto> Data { get; set; } = new();
    }
}
