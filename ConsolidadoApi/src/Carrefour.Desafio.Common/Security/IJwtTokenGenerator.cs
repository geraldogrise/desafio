namespace Carrefour.Desafio.Common.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IUser consolidado);
    }
}
