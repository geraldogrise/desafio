using Carrefour.Desafio.Domain.Entities;
using Carrefour.Desafio.Domain.Enums;

namespace Carrefour.Desafio.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
