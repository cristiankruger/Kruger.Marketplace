using Kruger.Marketplace.Business.Models.CadastroBasico;

namespace Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetByCategoriaId(Guid categoriaId);
    }
}
