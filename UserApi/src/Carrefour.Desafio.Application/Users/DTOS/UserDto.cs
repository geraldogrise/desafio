using Carrefour.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Users.DTOS
{

    /// <summary>
    /// Represents a user in the GetAllUsers response
    /// </summary>
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        private UserRole UserRole { get; set; }
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// Gets or sets the role as a string.
        /// </summary>
        public string Role
        {
            get => UserRole.ToString();
            set => UserRole = Enum.Parse<UserRole>(value, true);
        }


        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public string Status
        {
            get => UserStatus.ToString();
            set => UserStatus = Enum.Parse<UserStatus>(value, true);
        }


    }
}
