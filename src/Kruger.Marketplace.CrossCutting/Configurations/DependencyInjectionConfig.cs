using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Kruger.Marketplace.Business.Interfaces.Repositories;
using Kruger.Marketplace.Data.Context;
using Kruger.Marketplace.Data.Repositories;
using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Data.Repositories.CadastroBasico;
using Kruger.Marketplace.CrossCutting.Configurations.Swagger;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Services.CadastroBasico;
using System.Diagnostics.CodeAnalysis;
using Kruger.Marketplace.CrossCutting.App;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Kruger.Marketplace.Business.Services.Arquivo;
using Kruger.Marketplace.Business.Interfaces.Services.Arquivo;

namespace Kruger.Marketplace.CrossCutting.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependecies(this IServiceCollection services, bool configureForApi = true)
        {
            #region APPServices
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IdentityDbContext, AppDbContext>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            if (configureForApi)
                services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            #endregion         

            #region SERVICES
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IVendedorService, VendedorService>();
            #endregion

            #region REPOSITORIES
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();
            #endregion

            return services;
        }
    }
}
