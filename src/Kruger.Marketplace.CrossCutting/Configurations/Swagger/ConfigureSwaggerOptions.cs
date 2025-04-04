using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Kruger.Marketplace.CrossCutting.Configurations.Swagger
{
    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider = provider;        

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Kruger | Kruger Marketplace API",
                Description = "MBA DevXpert Módulo 1 - Kruger Marketplace API.",
                Version = "Beta",//VersionInfo.GetVersionInfo(),
                Contact = new OpenApiContact() { Name = "Cristian Kruger", Email = "Cristian.kruger@live.com" }
            };

            if (description.IsDeprecated)
                info.Description += " Esta versão está obsoleta!";

            return info;
        }
    }
}

