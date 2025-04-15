using Kruger.Marketplace.Business.Interfaces.Repositories;
using Kruger.Marketplace.Business.Models;
using Kruger.Marketplace.Data.Context;
using Kruger.Marketplace.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Data.Repositories
{
    public abstract class Repository<TEntity>(AppDbContext db) : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext Db = db;
        protected readonly DbSet<TEntity> DbSet = db.Set<TEntity>();

        #region READ
        public virtual async Task<int> GetTotal(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).AsNoTracking().CountAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, int pageNumber, int pageSize, bool desc)
        {
            return await DbSet.Where(predicate)
                              .OrderByCustom(orderBy, desc)
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .AsNoTracking()
                              .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate)
                              .AsNoTracking()
                              .ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        #endregion

        #region WRITE
        public virtual async Task<int> Add(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return 1;
        }

        public virtual async Task<int> Update(TEntity entity)
        {
            var e = await DbSet.FindAsync(entity.Id);
            Db.Entry(e).CurrentValues.SetValues(entity);
            return 1;
        }

        public virtual async Task<int> Delete(Guid id)
        {
            var e = await DbSet.FindAsync(id);
            DbSet.Remove(e);
            return 1;
        }

        public virtual async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        #endregion

        #region DISPOSE
        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
