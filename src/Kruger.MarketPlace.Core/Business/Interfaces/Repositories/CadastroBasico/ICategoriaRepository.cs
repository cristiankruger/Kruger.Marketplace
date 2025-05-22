using Kruger.Marketplace.Core.Business.Interfaces.Repositories;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;

namespace Kruger.Marketplace.Core.Business.Interfaces.Repositories.CadastroBasico
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetAll();
    }
}
