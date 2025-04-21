using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Kruger.Marketplace.Data.Context;
using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Data.Repositories.CadastroBasico;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Services.CadastroBasico;
using System.Diagnostics.CodeAnalysis;
using Kruger.Marketplace.Application.App;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Kruger.Marketplace.Business.Services.Arquivo;
using Kruger.Marketplace.Business.Interfaces.Services.Arquivo;
using Microsoft.AspNetCore.Builder;

namespace Kruger.Marketplace.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder ResolveDependecies(this WebApplicationBuilder builder)
        {
            #region APPServices
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IdentityDbContext, AppDbContext>();
            builder.Services.AddScoped<INotificador, Notificador>();
            builder.Services.AddScoped<IAppIdentityUser, AppIdentityUser>();                        
            #endregion         

            #region SERVICES
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IArquivoService, ArquivoService>();
            builder.Services.AddScoped<IVendedorService, VendedorService>();
            #endregion

            #region REPOSITORIES
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
            #endregion

            return builder;
        }
    }
}
