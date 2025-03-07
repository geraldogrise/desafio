
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Hosting;
using NSubstitute;

namespace Carrefour.Desafio.Integration.Core
{
   public class AmbevWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remover serviços reais (caso precise substituir)
                services.RemoveAll<IMediator>();

                // Criar mocks se necessário (exemplo para o MediatR)
                var mediatorSubstitute = Substitute.For<IMediator>();
                services.AddSingleton(mediatorSubstitute);
            });
        }
    }
}
