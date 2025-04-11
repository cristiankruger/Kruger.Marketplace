#nullable disable

using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kruger.Marketplace.Data.Repositories.CadastroBasico
{
    public class CategoriaRepository(AppDbContext context) : Repository<Categoria>(context), ICategoriaRepository
    {
        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await Db.Categorias.AsNoTracking().ToListAsync();
        }
    }
}
