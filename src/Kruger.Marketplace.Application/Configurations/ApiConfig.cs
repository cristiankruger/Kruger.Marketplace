using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using Kruger.Marketplace.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace Kruger.Marketplace.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class ApiConfig
    {
        #region IServiceCollection
        public static IServiceCollection AddApiBehaviorConfig(this IServiceCollection services)
        {
            services.AddControllers()
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressModelStateInvalidFilter = true;
                    });

            return services;
        }

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Default", builder => builder.AllowAnyOrigin()
                                                               .AllowAnyMethod()
                                                               .AllowAnyHeader());

                options.AddPolicy("Production", builder => builder.AllowAnyMethod()
                                                                  .WithOrigins("https://kruger.marketplace.com/")
                                                                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                                  .AllowAnyHeader()
                                                                  );
            });
            
            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddErrorDescriber<IdentityErrorsConfig>();            

            return services;
        }
        #endregion

        #region IApplicationBuilder
        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            if (!env.IsDevelopment())
            {
                app.UseCors("Production");
                app.UseHsts();
            }
            else
            {
                app.UseCors("Default");
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseMiddleware<ExceptionMiddleware>()
               .UseMiddleware<SecurityMiddleware>(env)
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization();

            return app;
        }

        public static IApplicationBuilder UseEndPointsConfiguration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
            return app;
        }
        #endregion
    }
}
