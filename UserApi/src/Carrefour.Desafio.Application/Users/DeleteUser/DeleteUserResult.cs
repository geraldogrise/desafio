using Carrefour.Desafio.Application.Users.DTOS;
using Carrefour.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrefour.Desafio.Application.Users.DeleteUser
{
    public class DeleteUserResult
    {
        /// <summary>
        /// Indicates whether the deletion was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the newly created user.
        /// </summary>
        /// <value>A GUID that uniquely identifies the created user in the system.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user to be created.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number for the user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public string Status
        {
            get => UserStatus.ToString();
            set => UserStatus = Enum.Parse<UserStatus>(value, true);
        }

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
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
        /// Gets or sets the role of the user.
        /// </summary>
        private UserRole UserRole { get; set; }

    }
}
