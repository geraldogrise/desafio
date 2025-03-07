using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// Represents a request to update an existing user in the system.
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// The unique identifier of the user to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username. Must be unique and contain only valid characters.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address. Must be a valid email format.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated status of the user account.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the updated role assigned to the user.
        /// </summary>
        public UserRole Role { get; set; }

      
    }
}
