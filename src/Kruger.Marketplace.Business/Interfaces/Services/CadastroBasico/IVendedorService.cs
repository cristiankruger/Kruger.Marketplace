using Kruger.Marketplace.Business.Models.CadastroBasico;

namespace Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico
{
    public interface IVendedorService : IDisposable
    {        
        Task<Vendedor> GetById(Guid id);
        Task<bool> Add(Vendedor vendedor);     
        Task SaveChanges();
    }
}
