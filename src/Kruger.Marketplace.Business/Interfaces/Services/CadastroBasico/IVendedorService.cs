using Kruger.Marketplace.Business.Models.CadastroBasico;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico
{
    public interface IVendedorService : IDisposable
    {
        Task<IEnumerable<Vendedor>> GetAll(Expression<Func<Vendedor, bool>> predicate, Expression<Func<Vendedor, object>> orderBy, int pageNumber, int pageSize, bool desc);
        Task<Vendedor> GetById(Guid id);
        Task<bool> Add(Vendedor vendedor);
        Task<bool> Update(Vendedor vendedor);
        Task<bool> Delete(Guid id);
        Task Complete();
    }
}
