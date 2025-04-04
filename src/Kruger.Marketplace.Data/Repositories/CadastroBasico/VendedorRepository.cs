using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Data.Context;

namespace Kruger.Marketplace.Data.Repositories.CadastroBasico
{
    public class VendedorRepository(AppDbContext context) : Repository<Vendedor>(context), IVendedorRepository
    {
        
    }
}
