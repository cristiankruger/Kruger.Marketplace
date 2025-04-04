using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;

namespace Kruger.Marketplace.Business.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriaRepository CategoriaRepository { get; }
        IProdutoRepository ProdutoRepository { get; }
        IVendedorRepository VendedorRepository { get; }

        Task<int> SaveChanges();
    }
}
