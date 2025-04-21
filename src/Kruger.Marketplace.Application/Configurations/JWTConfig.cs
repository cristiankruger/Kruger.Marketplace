using Kruger.Marketplace.Business.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Kruger.Marketplace.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class JWTConfig
    {
        public static WebApplicationBuilder AddJWTConfiguration(this WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection(AppSettings.ConfigName).Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Jwt);

            builder.Services
                   .AddAuthentication(t =>
                   {
                       t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   })
                   .AddJwtBearer(t =>
                   {
                       t.RequireHttpsMetadata = true;
                       t.SaveToken = true;
                       t.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(key),
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidAudiences = appSettings.ValidoEm,
                           ValidIssuer = appSettings.Emissor,
                           ClockSkew = TimeSpan.Zero
                       };
                   });

            return builder;
        }
    }
}
