using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kruger.Marketplace.Application.Extensions
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void ConfigureServices(IServiceCollection services, IWebHostEnvironment env);
        void Configure(WebApplication app, IWebHostEnvironment env);
    }
}
