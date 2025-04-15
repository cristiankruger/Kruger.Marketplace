using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using LinqKit;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Services.CadastroBasico
{
    public class CategoriaService(ICategoriaRepository categoriaRepository,
                                  IProdutoRepository produtoRepository,
                                  INotificador notificador) : BaseService(notificador), ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        #region READ

        public async Task<int> GetTotal(Expression<Func<Categoria, bool>> predicate)
        {
            return await _categoriaRepository.GetTotal(predicate);
        }

        public async Task<IEnumerable<Categoria>> GetAll(Expression<Func<Categoria, bool>> predicate, Expression<Func<Categoria, object>> orderBy, int pageNumber, int pageSize, bool desc)
        {
            return await _categoriaRepository.GetAll(predicate, orderBy, pageNumber, pageSize, desc);
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _categoriaRepository.GetAll();
        }

        public async Task<Categoria> GetById(Guid id)
        {
            return await _categoriaRepository.GetById(id);
        }
        #endregion

        #region WRITE
        public async Task<bool> Add(Categoria categoria)
        {
            if (!Validate(categoria, true)) return false;

            await _categoriaRepository.Add(categoria);

            return true;
        }

        public async Task<bool> Update(Categoria categoria)
        {
            if (!Validate(categoria)) return false;

            await _categoriaRepository.Update(categoria);

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var categoria = await _categoriaRepository.GetById(id);

            if (categoria is null) return NotificarError("Categoria não encontrada.");

            if (_produtoRepository.Search(p => p.CategoriaId == categoria.Id).Result.Any())
                return NotificarError("Não é possível excluir uma Categoria vinculada a produtos.");

            await _categoriaRepository.Delete(id);

            return true;
        }
        #endregion

        #region METHODS
        public void Dispose()
        {
            _categoriaRepository?.Dispose();
            _produtoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            await _categoriaRepository.SaveChanges();
        }

        private bool Validate(Categoria categoria, bool isInsert = false)
        {
            if (!IsValid(categoria)) return false;

            var expression = PredicateBuilder.New<Categoria>(m => m.Nome == categoria.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != categoria.Id);

            if (_categoriaRepository.Search(expression).Result.Any())
                return NotificarError("Categoria já cadastrada.");

            return true;
        }
        #endregion
    }
}
