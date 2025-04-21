using Kruger.Marketplace.Business.Models.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SettingsConfig
    {
        public static WebApplicationBuilder AddAppSettingsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AppSettings>(
                builder.Configuration.GetSection(AppSettings.ConfigName)
            );

            return builder;
        }

        public static WebApplicationBuilder AddArquivoSettingsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ArquivoSettings>(
                builder.Configuration.GetSection(ArquivoSettings.ConfigName)
            );

            return builder;
        }
    }
}
