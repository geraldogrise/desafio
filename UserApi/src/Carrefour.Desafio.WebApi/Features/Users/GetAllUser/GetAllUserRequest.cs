namespace Carrefour.Desafio.WebApi.Features.Users.GetAllUser
{
    /// <summary>
    /// Request model for retrieving all users.
    /// </summary>
    public class GetAllUsersRequest
    {
        /// <summary>
        /// The page number to retrieve (for pagination).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// The sorting order (e.g., ASC or DESC).
        /// </summary>
        public string Order { get; set; } = "ASC";
    }
}
