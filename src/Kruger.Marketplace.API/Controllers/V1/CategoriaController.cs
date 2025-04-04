using Asp.Versioning;
using AutoMapper;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.CrossCutting.Expressions;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Categoria;
using Kruger.Marketplace.CrossCutting.ViewModels.Pagina;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Kruger.Marketplace.API.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiversion}/[controller]")]
    public class CategoriaController(ICategoriaService categoriaService,
                                     IMapper mapper,
                                     INotificador notificador) : MainController(notificador)
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoriaService _categoriaService = categoriaService;

        #region READ
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoriaFilter filter)
        {
            var predicate = _mapper.Map<Expression<Func<Categoria, bool>>>(ControllerExpression.GetFilterExpression(filter));
            int totalRecords = await _categoriaService.GetTotal(predicate);

            var paged = new PagedViewModel<CategoriaViewModel>
            {
                TotalRecords = totalRecords,
                PagedData = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaService.GetAll(predicate,
                                                                                                        ControllerExpression.GetOrderByExpression(filter),
                                                                                                        filter.PageNumber,
                                                                                                        filter.PageSize,
                                                                                                        filter.Desc))
            };

            return CustomResponse(paged);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return CustomResponse(await Get(id));
        }
        #endregion

        #region WRITE
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _categoriaService.Add(_mapper.Map<Categoria>(categoriaViewModel)))
                return CustomResponse();

            await SaveChanges();
            return CustomResponse(categoriaViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CategoriaViewModel categoriaViewModel)
        {
            if (categoriaViewModel is null || id != categoriaViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo passado na query.");
                return CustomResponse(categoriaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _categoriaService.Update(_mapper.Map<Categoria>(categoriaViewModel)))
                return CustomResponse();

            await SaveChanges();
            return CustomResponse(categoriaViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _categoriaService.Delete(id))
                return CustomResponse();

            await SaveChanges();
            return CustomResponse($"categoria excluída.");
        }
        #endregion

        #region PRIVATE_METHODS       
        private async Task<CategoriaViewModel> Get(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaService.GetById(id));
        }

        private async Task SaveChanges()
        {
            await _categoriaService.SaveChanges();
        }
        #endregion

    }
}

