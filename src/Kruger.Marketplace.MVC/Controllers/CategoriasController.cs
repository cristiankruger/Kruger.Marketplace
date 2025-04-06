using Microsoft.AspNetCore.Mvc;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Categoria;
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
    //[Authorize]
    [Route("categorias")]
    public class CategoriasController(ICategoriaService categoriaService,
                                      IMapper mapper,
                                      INotificador notificador,
                                      IAppIdentityUser user) : MainController(notificador, user)
    {
        private readonly ICategoriaService _categoriaService = categoriaService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index(string busca, int pageSize = 10, int pageNumber = 1, string orderBy = "nome")
        {
            var filter = new CategoriaFilter()
            {
                Busca = busca,
                PageNumber = pageNumber,
                PageSize = pageSize,
                OrderBy = orderBy
            };

            var predicate = ControllerExpression.GetFilterExpression(filter);
            var orderExpression = ControllerExpression.GetOrderByExpression(filter);

            var paged = new PagedViewModel<CategoriaViewModel>
            {
                PageSizeList = GetPageSizeList(),
                Filter = filter,
                TotalRecords = await _categoriaService.GetTotal(predicate),
                PagedData = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaService.GetAll(predicate,
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
        public async Task<IActionResult> Create([Bind("Nome,Descricao,Id")] CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
                return View(categoriaViewModel);

            if (!await _categoriaService.Add(_mapper.Map<Categoria>(categoriaViewModel)))
            {
                GetErrorsFromNotificador();

                return View(categoriaViewModel);
            }

            await _categoriaService.SaveChanges();

            TempData["Sucesso"] = "Categoria Cadastrada.";

            return RedirectToAction(nameof(Index));
        }

        [Route("editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
            return await GetById(id);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,Id")] CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(categoriaViewModel);

            if (!await _categoriaService.Update(_mapper.Map<Categoria>(categoriaViewModel)))
            {
                GetErrorsFromNotificador();

                return View(categoriaViewModel);
            }

            await _categoriaService.SaveChanges();

            TempData["Sucesso"] = "Categoria Editada.";

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
            var categoriaViewModel = await GetById(id);

            if (!await _categoriaService.Delete(id))
            {
                GetErrorsFromNotificador();

                return View(categoriaViewModel);
            }

            await _categoriaService.SaveChanges();

            TempData["Sucesso"] = "Categoria Excluída.";

            return RedirectToAction(nameof(Index));
        }

        #region PRIVATE METHODS
       

        private async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var categoriaViewModel = _mapper.Map<CategoriaViewModel>(await _categoriaService.GetById(id));

            if (categoriaViewModel is null)
                   return NotFound();

            return View(categoriaViewModel);
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
