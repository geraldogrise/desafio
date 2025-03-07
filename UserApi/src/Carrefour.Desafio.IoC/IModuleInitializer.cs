using Microsoft.AspNetCore.Builder;

namespace Carrefour.Desafio.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
