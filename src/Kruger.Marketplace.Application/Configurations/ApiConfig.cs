using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Asp.Versioning;
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
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
           
            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddVersioningConfig(this IServiceCollection services)
        {
            services
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                        new HeaderApiVersionReader("x-api-version"),
                                                                        new MediaTypeApiVersionReader("x-api-version"));
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
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

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<SecurityMiddleware>(env);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

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
