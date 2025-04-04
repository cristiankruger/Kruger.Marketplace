using Kruger.Marketplace.CrossCutting.Configurations;
using IStartup = Kruger.Marketplace.CrossCutting.Extensions.IStartup;

namespace Kruger.Marketplace.MVC
{
    public class Startup(IConfiguration configuration) : IStartup
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration);
            services.AddAppSettingsConfiguration(Configuration);
            services.AddAutoMapper(typeof(AutomapperConfig));
            services.ResolveDependecies(false);
            services.MvcBehaviorConfig();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.MigrateDatabase();
            app.UseMvcConfiguration();
        }
    }

}