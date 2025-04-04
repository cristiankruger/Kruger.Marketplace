using Microsoft.AspNetCore.Builder;

namespace Kruger.Marketplace.CrossCutting.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder webAppBuilder)
            where TStartup : IStartup
        {
            if (Activator.CreateInstance(typeof(TStartup), webAppBuilder.Configuration) is not IStartup startup)
                throw new ArgumentException("classe startup.cs inválida!");

            startup.ConfigureServices(webAppBuilder.Services);

            var app = webAppBuilder.Build();

            startup.Configure(app, app.Environment);

            app.Run();

            return webAppBuilder;
        }
    }
}