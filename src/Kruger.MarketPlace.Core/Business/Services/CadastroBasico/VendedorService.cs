using Kruger.Marketplace.Core.Business.Interfaces.Notificador;
using Kruger.Marketplace.Core.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Core.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;
using Kruger.Marketplace.Core.Business.Services;
using LinqKit;

namespace Kruger.Marketplace.Core.Business.Services.CadastroBasico
{
    public class VendedorService(IVendedorRepository vendedorRepository,
                                 INotificador notificador) : BaseService(notificador), IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository = vendedorRepository;

        #region READ
        public async Task<Vendedor> GetById(Guid id)
        {
            return await _vendedorRepository.GetById(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Add(Vendedor vendedor)
        {
            if (!Validate(vendedor, true)) return false;

            await _vendedorRepository.Add(vendedor);

            return true;
        }      
        #endregion

        #region METHODS
        public void Dispose()
        {
            _vendedorRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            await _vendedorRepository.SaveChanges();
        }

        private bool Validate(Vendedor vendedor, bool isInsert = false)
        {
            if (!IsValid(vendedor)) return false;

            var expression = PredicateBuilder.New<Vendedor>(m => m.Nome == vendedor.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != vendedor.Id);

            if (_vendedorRepository.Search(expression).Result.Any())
                return NotificarError("Vendedor já cadastrado.");

            return true;
        }
        #endregion
    }
}
