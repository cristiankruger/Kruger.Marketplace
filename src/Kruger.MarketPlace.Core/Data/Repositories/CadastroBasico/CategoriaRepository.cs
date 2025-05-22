using Kruger.Marketplace.Core.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;
using Kruger.Marketplace.Core.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kruger.Marketplace.Core.Data.Repositories.CadastroBasico
{
    public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
    {
        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await Db.Categorias.AsNoTracking().ToListAsync();
        }
    }
}
