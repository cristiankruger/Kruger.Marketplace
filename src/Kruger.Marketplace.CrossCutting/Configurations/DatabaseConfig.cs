using Kruger.Marketplace.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.CrossCutting.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {

            if (env.IsDevelopment())
                services.AddDbContext<AppDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnectionLite"),
                                                                                 opt => opt.CommandTimeout(45)
                                                                                           .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            else
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                    opt => opt.CommandTimeout(45)
                                                                                              .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                              .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

            return services;
        }

        public static async Task<IApplicationBuilder> MigrateDatabase(this IApplicationBuilder app)
        {
            using var appContext = GetDbContext(app);
            appContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
            await appContext.Database.MigrateAsync();
         
            return app;
        }

        private static AppDbContext GetDbContext(this IApplicationBuilder app)
        {
            return app.ApplicationServices
                      .GetRequiredService<IServiceScopeFactory>()
                      .CreateScope()
                      .ServiceProvider
                      .GetService<AppDbContext>();
        }        
    }
}
