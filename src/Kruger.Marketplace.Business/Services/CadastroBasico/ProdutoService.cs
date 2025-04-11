using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Repositories;
using Kruger.Marketplace.Business.Interfaces.Services.Arquivo;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using LinqKit;
using Microsoft.AspNetCore.Hosting;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Services.CadastroBasico
{
    public class ProdutoService(IUnitOfWork unitOfWork,
                                INotificador notificador,                                
                                IArquivoService arquivoService) : BaseService(notificador), IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IArquivoService _arquivoService = arquivoService;        

        #region READ
        public async Task<int> GetTotal(Expression<Func<Produto, bool>> predicate)
        {
            return await _unitOfWork.ProdutoRepository.GetTotal(predicate);
        }

        public async Task<IEnumerable<Produto>> GetAll(Expression<Func<Produto, bool>> predicate, Expression<Func<Produto, object>> orderBy, int pageNumber, int pageSize, bool desc)
        {
            return await _unitOfWork.ProdutoRepository.GetAll(predicate, orderBy, pageNumber, pageSize, desc);
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _unitOfWork.ProdutoRepository.GetById(id);            
        }
        #endregion

        #region WRITE
        public async Task<bool> Add(Produto produto)
        {
            if (!Validate(produto, true)) return false;

            await HandleFile(produto, true);

            await _unitOfWork.ProdutoRepository.Add(produto);

            return true;
        }

        public async Task<bool> Update(Produto produto)
        {
            if (!Validate(produto)) return false;

            await HandleFile(produto, false);

            await _unitOfWork.ProdutoRepository.Update(produto);

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var produto = await _unitOfWork.ProdutoRepository.GetById(id);

            if (produto is null) return NotificarError("Produto não encontrado.");

            _arquivoService.Delete(produto.Imagem);

            await _unitOfWork.ProdutoRepository.Delete(id);

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

        private bool Validate(Produto produto, bool isInsert = false)
        {
            if (!IsValid(produto)) return false;

            var expression = PredicateBuilder.New<Produto>(m => m.Nome == produto.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != produto.Id);

            if (_unitOfWork.ProdutoRepository.Search(expression).Result.Any())
                return NotificarError("Produto já cadastrado.");

            return true;
        }

        private async Task<bool> HandleFile(Produto produto, bool isInsert = true)
        {
            if (!await _arquivoService.Save(produto.Imagem, produto.FileUpload))
                return NotificarError("Erro ao salvar arquivo.");

            if (!isInsert )
            {
                var current = await _unitOfWork.ProdutoRepository.GetById(produto.Id);

                if (current is not null && current.Imagem != produto.Imagem)
                    _arquivoService.Delete(current.Imagem);
            }
         
            return true;
        }
        #endregion
    }
}
