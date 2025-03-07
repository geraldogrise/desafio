using Carrefour.Desafio.Domain.Entities;

namespace Carrefour.Desafio.Domain.Events
{
    public class UserRegisteredEvent
    {
        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
        }
    }
}
