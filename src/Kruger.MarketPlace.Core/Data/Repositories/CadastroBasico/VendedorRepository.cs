using Kruger.Marketplace.Core.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;
using Kruger.Marketplace.Core.Data.Context;

namespace Kruger.Marketplace.Core.Data.Repositories.CadastroBasico
{
    public class VendedorRepository(AppDbContext context) : Repository<Vendedor>(context), IVendedorRepository
    {
        
    }
}
