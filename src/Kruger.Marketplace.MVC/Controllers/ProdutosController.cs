using Microsoft.AspNetCore.Mvc;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Produto;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using AutoMapper;
using Kruger.Marketplace.Application.ViewModels.Pagina;
using Kruger.Marketplace.Application.Expressions;
using Microsoft.AspNetCore.Authorization;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Categoria;
using Microsoft.Extensions.Options;
using Kruger.Marketplace.Business.Models.Settings;
using LinqKit;

namespace Kruger.Marketplace.MVC.Controllers
{
    [Authorize]
    [Route("produtos")]
    public class ProdutosController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly ArquivoSettings _arquivoSettings;
        private readonly string imageBasePath;

        public ProdutosController(IProdutoService produtoService,
                                  ICategoriaService categoriaService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IAppIdentityUser user,
                                  IOptions<ArquivoSettings> arquivoSettings) : base(notificador, user)
        {
            _mapper = mapper;
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _arquivoSettings = arquivoSettings.Value;
            imageBasePath = $"{_arquivoSettings.BasePath}{_arquivoSettings.Container}";
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string busca, int pageSize = 10, int pageNumber = 1, string orderBy = "nome")
        {
            var filter = new ProdutoFilter()
            {
                Busca = busca,
                PageNumber = pageNumber,
                PageSize = pageSize,
                OrderBy = orderBy,
                VendedorId = UserId
            };

            var predicate = ControllerExpression.GetFilterExpression(filter);
            var orderExpression = ControllerExpression.GetOrderByExpression(filter);

            var paged = new PagedViewModel<ProdutoViewModel>
            {
                PageSizeList = GetPageSizeList(pageSize),
                Filter = filter,
                TotalRecords = await _produtoService.GetTotal(predicate),
                PagedData = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoService.GetAll(predicate,
                                                                                                    orderExpression,
                                                                                                    filter.PageNumber,
                                                                                                    filter.PageSize,
                                                                                                    filter.Desc))
            };

            //paged.PagedData.ForEach(produtoViewModel => produtoViewModel.SetImageProperties(imageBasePath, _arquivoSettings.DefaultImage));

            return View(paged);
        }

        [AllowAnonymous]
        [Route("detalhes/{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return await GetById(id);
        }

        [Route("novo")]
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularCategorias(new ProdutoViewModel());
            produtoViewModel.SetImageProperties(imageBasePath, _arquivoSettings.DefaultImage);
            return View(produtoViewModel);
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id,Estoque,Preco,CategoriaId,VendedorId,Imagem,FileUpload")] ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularCategorias(produtoViewModel);
            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(imageBasePath, _arquivoSettings.DefaultImage);

            if (!ModelState.IsValid)
                return View(produtoViewModel);

            if (!await _produtoService.Add(_mapper.Map<Produto>(produtoViewModel)))
            {
                GetErrorsFromNotificador();

                return View(produtoViewModel);
            }

            await _produtoService.SaveChanges();

            TempData["Sucesso"] = "Produto Cadastrado.";

            return RedirectToAction(nameof(Index));
        }

        [Route("editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return await GetById(id);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,Id,Estoque,Preco,CategoriaId,VendedorId,Imagem,FileUpload")] ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(produtoViewModel);

            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(imageBasePath, produtoViewModel.Imagem);

            if (!await _produtoService.Update(_mapper.Map<Produto>(produtoViewModel)))
            {
                GetErrorsFromNotificador();

                produtoViewModel = await PopularCategorias(produtoViewModel);

                return View(produtoViewModel);
            }

            await _produtoService.SaveChanges();

            TempData["Sucesso"] = "Produto Editado.";

            return RedirectToAction(nameof(Index));
        }

        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await GetById(id);
        }

        [HttpPost("excluir/{id:guid}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await GetById(id);

            if (!await _produtoService.Delete(id))
            {
                GetErrorsFromNotificador();

                return View();
            }

            await _produtoService.SaveChanges();

            TempData["Sucesso"] = "Produto Excluído.";

            return RedirectToAction(nameof(Index));
        }

        #region PRIVATE METHODS
        private async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(await _produtoService.GetById(id));

            if (produtoViewModel is null)
                return NotFound();

            produtoViewModel = await PopularCategorias(produtoViewModel);
            produtoViewModel.SetImageProperties(imageBasePath, produtoViewModel.Imagem);

            return View(produtoViewModel);
        }

        private async Task<ProdutoViewModel> PopularCategorias(ProdutoViewModel produto)
        {
            produto.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaService.GetAll());
            return produto;
        }
        #endregion
    }
}
