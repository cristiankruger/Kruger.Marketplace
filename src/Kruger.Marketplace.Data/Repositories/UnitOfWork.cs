using Kruger.Marketplace.Business.Interfaces.Repositories;
using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Data.Context;
using Kruger.Marketplace.Data.Repositories.CadastroBasico;
using Microsoft.EntityFrameworkCore;

namespace Kruger.Marketplace.Data.Repositories
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;

        private ICategoriaRepository _categoriaRepository;
        public ICategoriaRepository CategoriaRepository { get => _categoriaRepository ??= new CategoriaRepository(_dbContext); }

        
        private IProdutoRepository _produtoRepository;
        public IProdutoRepository ProdutoRepository { get => _produtoRepository ??= new ProdutoRepository(_dbContext); }

        private IVendedorRepository _vendedorRepository;
        public IVendedorRepository VendedorRepository { get => _vendedorRepository ??= new VendedorRepository(_dbContext); }

        public async Task<int> SaveChanges()
        {
            var saved = 0;

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                var transaction = _dbContext.Database.BeginTransaction();
                saved = await _dbContext.SaveChangesAsync();
                transaction.Commit();
            });

            return saved;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
