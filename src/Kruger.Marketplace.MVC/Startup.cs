using Kruger.Marketplace.CrossCutting.Configurations;
using IStartup = Kruger.Marketplace.CrossCutting.Extensions.IStartup;

namespace Kruger.Marketplace.MVC
{
    public class Startup(IConfiguration configuration) : IStartup
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddDatabase(Configuration, env)
                    .AddAppSettingsConfiguration(Configuration)
                    .AddArquivoSettingsConfiguration(Configuration)
                    .AddAutoMapper(typeof(AutomapperConfig))
                    .MvcBehaviorConfig()
                    .ResolveDependecies(false);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.MigrateDatabase().Wait();
            app.UseMvcConfiguration();
        }
    }

}