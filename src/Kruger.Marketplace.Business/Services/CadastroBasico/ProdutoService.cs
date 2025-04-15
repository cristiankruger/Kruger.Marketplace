using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Repositories.CadastroBasico;
using Kruger.Marketplace.Business.Interfaces.Services.Arquivo;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using LinqKit;
using System.Linq.Expressions;

namespace Kruger.Marketplace.Business.Services.CadastroBasico
{
    public class ProdutoService(IProdutoRepository produtoRepository,
                                INotificador notificador,                                
                                IArquivoService arquivoService) : BaseService(notificador), IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IArquivoService _arquivoService = arquivoService;        

        #region READ
        public async Task<int> GetTotal(Expression<Func<Produto, bool>> predicate)
        {
            return await _produtoRepository.GetTotal(predicate);
        }

        public async Task<IEnumerable<Produto>> GetAll(Expression<Func<Produto, bool>> predicate, Expression<Func<Produto, object>> orderBy, int pageNumber, int pageSize, bool desc)
        {
            return await _produtoRepository.GetAll(predicate, orderBy, pageNumber, pageSize, desc);
        }

        public async Task<Produto> GetById(Guid id)
        {
            return await _produtoRepository.GetById(id);            
        }
        #endregion

        #region WRITE
        public async Task<bool> Add(Produto produto)
        {
            if (!Validate(produto, true)) return false;

            await HandleFile(produto, true);

            await _produtoRepository.Add(produto);

            return true;
        }

        public async Task<bool> Update(Produto produto)
        {
            if (!Validate(produto)) return false;

            await HandleFile(produto, false);

            await _produtoRepository.Update(produto);

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var produto = await _produtoRepository.GetById(id);

            if (produto is null) return NotificarError("Produto não encontrado.");

            _arquivoService.Delete(produto.Imagem);

            await _produtoRepository.Delete(id);

            return true;
        }
        #endregion

        #region METHODS
        public void Dispose()
        {
            _produtoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            await _produtoRepository.SaveChanges();
        }

        private bool Validate(Produto produto, bool isInsert = false)
        {
            if (!IsValid(produto)) return false;

            var expression = PredicateBuilder.New<Produto>(m => m.Nome == produto.Nome);
            if (!isInsert) expression = expression.And(m => m.Id != produto.Id);

            if (_produtoRepository.Search(expression).Result.Any())
                return NotificarError("Produto já cadastrado.");

            return true;
        }

        private async Task<bool> HandleFile(Produto produto, bool isInsert = true)
        {
            if (!await _arquivoService.Save(produto.Imagem, produto.FileUpload))
                return NotificarError("Erro ao salvar arquivo.");

            if (!isInsert )
            {
                var current = await _produtoRepository.GetById(produto.Id);

                if (current is not null && current.Imagem != produto.Imagem)
                    _arquivoService.Delete(current.Imagem);
            }
         
            return true;
        }
        #endregion
    }
}
