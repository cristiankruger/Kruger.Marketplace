﻿using Kruger.Marketplace.Business.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.CrossCutting.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SettingsConfig
    {
        public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(section);

            return services;
        }
    }
}
