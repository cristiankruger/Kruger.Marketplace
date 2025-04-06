using Kruger.Marketplace.Business.Models;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<int> GetTotal(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, int pageNumber, int pageSize, bool desc = false);        
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<int> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Delete(Guid id);
    }
}
