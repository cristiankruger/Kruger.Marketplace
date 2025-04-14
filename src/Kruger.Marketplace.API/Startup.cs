using Kruger.Marketplace.Application.Configurations;
using Kruger.Marketplace.Application.Configurations.Swagger;
using IStartup = Kruger.Marketplace.Application.Extensions.IStartup;

namespace Kruger.Marketplace.API
{
    public class Startup(IConfiguration configuration) : IStartup
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddDatabase(Configuration, env)
                    .AddAppSettingsConfiguration(Configuration)
                    .AddArquivoSettingsConfiguration(Configuration)
                    .AddJWTConfiguration(Configuration)
                    .AddAutoMapper(typeof(AutomapperConfig))
                    .AddApiBehaviorConfig()
                    .AddVersioningConfig()
                    .AddCorsConfig()
                    .AddEndpointsApiExplorer()
                    .AddSwaggerConfig()
                    .ResolveDependecies();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.MigrateDatabase().Wait();
            app.UseApiConfiguration(env);
            app.UseSwaggerConfig();
            app.UseEndPointsConfiguration();
        }
    }

}