using Microsoft.AspNetCore.Builder;

namespace Kruger.Marketplace.Application.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder)
            where TStartup : IStartup
        {
            if (Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration) is not IStartup startup)
                throw new ArgumentException("classe startup.cs inválida!");

            startup.ConfigureServices(webAppBuilder.Services, webAppBuilder.Environment);

            var app = webAppBuilder.Build();

            startup.Configure(app, app.Environment);

            app.Run();

            return webAppBuilder;
        }
    }
}