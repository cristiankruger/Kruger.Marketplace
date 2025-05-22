using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Kruger.Marketplace.Core.Business.Interfaces.Notificador;
using Kruger.Marketplace.Core.Business.Notificacoes;
using Kruger.Marketplace.Core.Data.Context;
using Kruger.Marketplace.Core.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Core.Data.Repositories.CadastroBasico;
using Kruger.Marketplace.Core.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Core.Business.Services.CadastroBasico;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Kruger.Marketplace.Core.Business.Services.Arquivo;
using Kruger.Marketplace.Core.Business.Interfaces.Services.Arquivo;
using Microsoft.AspNetCore.Builder;
using Kruger.Marketplace.Core.Application.App;

namespace Kruger.Marketplace.Core.Application.Configurations
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
