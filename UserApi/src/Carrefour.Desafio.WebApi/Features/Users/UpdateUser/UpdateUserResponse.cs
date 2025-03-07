using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// API response model for UpdateUser operation
    /// </summary>
    public class UpdateUserResponse
    {
        /// <summary>
        /// The unique identifier of the updated user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user's full name.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role as a string.
        /// </summary>
        public string Role
        {
            get => UserRole.ToString();
            set => UserRole = Enum.Parse<UserRole>(value, true);
        }

        /// <summary>
        /// The user's role in the system.
        /// </summary>
        private UserRole UserRole { get; set; }

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public string Status
        {
            get => UserStatus.ToString();
            set => UserStatus = Enum.Parse<UserStatus>(value, true);
        }

        /// <summary>
        /// The current status of the user.
        /// </summary>
        private UserStatus UserStatus { get; set; }

    }
}
