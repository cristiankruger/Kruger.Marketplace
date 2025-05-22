using Kruger.Marketplace.Core.Business.Models.CadastroBasico;

namespace Kruger.Marketplace.Core.Business.Interfaces.Services.CadastroBasico
{
    public interface IVendedorService : IDisposable
    {        
        Task<Vendedor> GetById(Guid id);
        Task<bool> Add(Vendedor vendedor);     
        Task SaveChanges();
    }
}
