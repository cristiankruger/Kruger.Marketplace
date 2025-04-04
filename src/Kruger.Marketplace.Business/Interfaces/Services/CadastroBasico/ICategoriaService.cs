using Kruger.Marketplace.Business.Models.CadastroBasico;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico
{
    public interface ICategoriaService : IDisposable
    {
        Task<int> GetTotal(Expression<Func<Categoria, bool>> predicate);
        Task<IEnumerable<Categoria>> GetAll(Expression<Func<Categoria, bool>> predicate, Expression<Func<Categoria, object>> orderBy, int pageNumber, int pageSize, bool desc = false);
        Task<Categoria> GetById(Guid id);
        Task<bool> Add(Categoria categoria);
        Task<bool> Update(Categoria categoria);
        Task<bool> Delete(Guid id);
        Task SaveChanges();
    }
}
