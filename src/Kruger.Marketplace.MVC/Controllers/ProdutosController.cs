﻿using Microsoft.AspNetCore.Mvc;
using Kruger.Marketplace.Core.Application.ViewModels.CadastroBasico.Produto;
using Kruger.Marketplace.Core.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Core.Business.Models.CadastroBasico;
using Kruger.Marketplace.Core.Business.Interfaces.Notificador;
using AutoMapper;
using Kruger.Marketplace.Core.Application.ViewModels.Pagina;
using Kruger.Marketplace.Core.Application.Expressions;
using Microsoft.AspNetCore.Authorization;
using Kruger.Marketplace.Core.Application.App;
using Kruger.Marketplace.Core.Application.ViewModels.CadastroBasico.Categoria;
using Microsoft.Extensions.Options;
using Kruger.Marketplace.Core.Business.Models.Settings;

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
            produtoViewModel.SetImageProperties(_arquivoSettings.DefaultImage);
            return View(produtoViewModel);
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id,Estoque,Preco,CategoriaId,VendedorId,Imagem,FileUpload")] ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularCategorias(produtoViewModel);
            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(_arquivoSettings.DefaultImage);

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
            produtoViewModel.SetImageProperties(produtoViewModel.Imagem);

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
            produtoViewModel.SetImageProperties(produtoViewModel.Imagem);

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
