using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Users.GetAllUser
{
    /// <summary>
    /// Command for retrieving a paginated list of users
    /// </summary>
    public class GetAllUsersCommand : IRequest<GetAllUsersResult>
    {
        /// <summary>
        /// The page number to retrieve
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of users per page
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The order criteria for sorting the results
        /// </summary>
        public string Order { get; set; } = "Name ASC";
    }
}
