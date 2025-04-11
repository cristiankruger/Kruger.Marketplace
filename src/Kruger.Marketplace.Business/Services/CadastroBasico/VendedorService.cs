using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Repositories;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using LinqKit;

namespace Kruger.Marketplace.Business.Services.CadastroBasico
{
    public class VendedorService(IUnitOfWork unitOfWork,
                                 INotificador notificador) : BaseService(notificador), IVendedorService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        #region READ
        public async Task<Vendedor> GetById(Guid id)
        {
            return await _unitOfWork.VendedorRepository.GetById(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Add(Vendedor vendedor)
        {
            if (!Validate(vendedor, true)) return false;

            await _unitOfWork.VendedorRepository.Add(vendedor);

            return true;
        }      
        #endregion

        #region METHODS
        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            await _unitOfWork.SaveChanges();
        }

        private bool Validate(Vendedor vendedor, bool isInsert = false)
        {
            if (!IsValid(vendedor)) return false;

            var expression = PredicateBuilder.New<Vendedor>(m => m.Nome == vendedor.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != vendedor.Id);

            if (_unitOfWork.VendedorRepository.Search(expression).Result.Any())
                return NotificarError("Vendedor já cadastrada.");

            return true;
        }
        #endregion
    }
}
