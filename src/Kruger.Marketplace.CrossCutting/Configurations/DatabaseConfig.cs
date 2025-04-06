using Kruger.Marketplace.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                opt => opt.CommandTimeout(45)
                                                                                          .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                          .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            //services.AddDbContext<AppDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnectionLite"),
            //                                                                 opt => opt.CommandTimeout(45)
            //                                                                           .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            return services;
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var context = app.ApplicationServices
                                   .GetRequiredService<IServiceScopeFactory>()
                                   .CreateScope()
                                   .ServiceProvider
                                   .GetService<AppDbContext>();

            context.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
            context.Database.Migrate();

            return app;
        }
    }
}
