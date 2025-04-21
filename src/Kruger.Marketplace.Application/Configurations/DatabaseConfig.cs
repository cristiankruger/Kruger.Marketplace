﻿using Kruger.Marketplace.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseConfig
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            var configuration = builder.Configuration;

            if (env.IsDevelopment())
                builder.Services
                       .AddDbContext<AppDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnectionLite"),
                                                                                opt => opt.CommandTimeout(45)
                                                                                          .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            else
                builder.Services
                       .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                   opt => opt.CommandTimeout(45)
                                                                                             .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                             .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            return builder;
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
