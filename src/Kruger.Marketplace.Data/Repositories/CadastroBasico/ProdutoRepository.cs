using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Data.Context;
using Kruger.Marketplace.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Data.Repositories.CadastroBasico
{
    public class ProdutoRepository(AppDbContext context) : Repository<Produto>(context), IProdutoRepository
    {
        public override async Task<IEnumerable<Produto>> GetAll(Expression<Func<Produto, bool>> predicate, Expression<Func<Produto, object>> orderBy, int pageNumber, int pageSize, bool desc)
        {
            return await Db.Produtos
                            .Include(p => p.Categoria)
                            .Include(p => p.Vendedor)
                            .Where(predicate)
                            .OrderByCustom(orderBy, desc)
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public override async Task<Produto> GetById(Guid id)
        {
            return await Db.Produtos
                            .Include(p => p.Categoria)
                            .Include(p => p.Vendedor)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaId(Guid categoriaId)
        {
            return await Db.Produtos
                            .Include(p => p.Categoria)
                            .Include(p => p.Vendedor)
                            .Where(p => p.CategoriaId == categoriaId)
                            .AsNoTracking()
                            .ToListAsync();
        }
    }
}
