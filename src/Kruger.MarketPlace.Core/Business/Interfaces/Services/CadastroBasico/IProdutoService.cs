using Kruger.Marketplace.Core.Business.Models.CadastroBasico;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Core.Business.Interfaces.Services.CadastroBasico
{
    public interface IProdutoService : IDisposable
    {
        Task<int> GetTotal(Expression<Func<Produto, bool>> predicate);
        Task<IEnumerable<Produto>> GetAll(Expression<Func<Produto, bool>> predicate, Expression<Func<Produto, object>> orderBy, int pageNumber, int pageSize, bool desc);
        Task<Produto> GetById(Guid id);
        Task<bool> Add(Produto produto);
        Task<bool> Update(Produto produto);
        Task<bool> Delete(Guid id);
        Task SaveChanges();
    }
}
