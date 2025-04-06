using Microsoft.AspNetCore.Mvc;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Produto;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using AutoMapper;
using Kruger.Marketplace.CrossCutting.ViewModels.Pagina;
using Microsoft.AspNetCore.Mvc.Rendering;
using Kruger.Marketplace.CrossCutting.Expressions;
using Microsoft.AspNetCore.Authorization;
using Kruger.Marketplace.CrossCutting.App;

namespace Kruger.Marketplace.MVC.Controllers
{
    [Authorize]
    [Route("produtos")]
    public class ProdutosController(IProdutoService produtoService,
                                    IMapper mapper,
                                    INotificador notificador,
                                    IAppIdentityUser user) : MainController(notificador, user)
    {
        private readonly IProdutoService _produtoService = produtoService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index(string busca, int pageSize = 10, int pageNumber = 1, string orderBy = "nome")
        {
            var filter = new ProdutoFilter()
            {
                Busca = busca,
                PageNumber = pageNumber,
                PageSize = pageSize,
                OrderBy = orderBy
            };

            var predicate = ControllerExpression.GetFilterExpression(filter);
            var orderExpression = ControllerExpression.GetOrderByExpression(filter);

            var paged = new PagedViewModel<ProdutoViewModel>
            {
                PageSizeList = GetPageSizeList(),
                Filter = filter,
                TotalRecords = await _produtoService.GetTotal(predicate),
                PagedData = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoService.GetAll(predicate,
                                                                                                    orderExpression,
                                                                                                    filter.PageNumber,
                                                                                                    filter.PageSize,
                                                                                                    filter.Desc))
            };

            return View(paged);
        }

        [Route("detalhes/{id:Guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            return await GetById(id);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id,Estoque,Preco,CategoriaId,VendedorId,FileUpload")] ProdutoViewModel produtoViewModel)
        {
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,Id,Estoque,Preco,CategoriaId,VendedorId,FileUpload")] ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(produtoViewModel);

            if (!await _produtoService.Update(_mapper.Map<Produto>(produtoViewModel)))
            {
                GetErrorsFromNotificador();

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

                return View(produtoViewModel);
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

            return View(produtoViewModel);
        }

        private List<SelectListItem> GetPageSizeList()
        {
            List<SelectListItem> listaPageSize =
            [
                new() { Value = "25", Text = "25" },
                new() { Value = "50", Text = "50" },
                new() { Value = "100", Text = "100" }
            ];

            return listaPageSize;
        }
        #endregion
    }
}
