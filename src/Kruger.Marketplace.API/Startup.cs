using Kruger.Marketplace.CrossCutting.Configurations;
using Kruger.Marketplace.CrossCutting.Configurations.Swagger;
using IStartup = Kruger.Marketplace.CrossCutting.Extensions.IStartup;

namespace Kruger.Marketplace.API
{
    public class Startup(IConfiguration configuration) : IStartup
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration);
            services.AddAppSettingsConfiguration(Configuration);
            services.AddJWTConfiguration(Configuration);
            services.AddAutoMapper(typeof(AutomapperConfig));
            services.ApiBehaviorConfig();
            services.ApiVersioningConfig();
            services.ApiCorsConfig();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfig();
            services.ResolveDependecies();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.MigrateDatabase();
            app.UseApiConfiguration(env);
            app.UseSwaggerConfig();
            app.UseEndPointsConfiguration();
        }
    }

}