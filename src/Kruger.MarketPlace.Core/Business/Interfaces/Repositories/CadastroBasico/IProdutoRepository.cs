using Kruger.Marketplace.Core.Business.Interfaces.Repositories;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;

namespace Kruger.Marketplace.Core.Business.Interfaces.Repositories.CadastroBasico
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetByCategoriaId(Guid categoriaId);
    }
}
