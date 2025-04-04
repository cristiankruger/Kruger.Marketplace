using Kruger.Marketplace.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.CrossCutting.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.ConfigureWarnings(wc => wc.Ignore(RelationalEventId.BoolWithDefaultWarning));
            });

            return services;
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var context = GetDbContextService(app);
            context.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
            context.Database.Migrate();

            return app;
        }

        #region PRIVATE_METHODS
        private static AppDbContext GetDbContextService(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            return scope.ServiceProvider.GetService<AppDbContext>();
        }
        #endregion
    }
}
