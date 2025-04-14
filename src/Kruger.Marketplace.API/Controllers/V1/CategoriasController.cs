using Asp.Versioning;
using AutoMapper;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.Expressions;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Categoria;
using Kruger.Marketplace.Application.ViewModels.Pagina;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using System.Net;

namespace Kruger.Marketplace.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiversion}/[controller]")]
    public class CategoriasController(ICategoriaService categoriaService,
                                      IMapper mapper,
                                      IAppIdentityUser user,
                                      INotificador notificador) : MainController(notificador, user)
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoriaService _categoriaService = categoriaService;

        #region READ
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

            return CustomResponse(HttpStatusCode.OK, paged);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var categoriaViewModel = await Get(id);

            if (categoriaViewModel is not null)
                return CustomResponse(HttpStatusCode.OK, categoriaViewModel);

            NotificarErro("Categoria não encontrada.");
            return CustomResponse(HttpStatusCode.NotFound);
        }
        #endregion

        #region WRITE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public async Task<IActionResult> Post([FromBody] CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _categoriaService.Add(_mapper.Map<Categoria>(categoriaViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await SaveChanges(categoriaViewModel.Id);

            return CustomResponse(HttpStatusCode.Created, null, categoriaViewModel, categoriaViewModel.Id);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] CategoriaViewModel categoriaViewModel)
        {
            if (categoriaViewModel is null || id != categoriaViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo passado na query.");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _categoriaService.Update(_mapper.Map<Categoria>(categoriaViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await SaveChanges(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _categoriaService.Delete(id))
                return CustomResponse(HttpStatusCode.BadRequest);

            await SaveChanges(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
        #endregion

        #region PRIVATE_METHODS       
        private async Task<CategoriaViewModel> Get(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _categoriaService.GetById(id));
        }

        private async Task SaveChanges(Guid id)
        {
            try
            {
                await _categoriaService.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                if (await Get(id) is not null)
                    throw;

                NotificarErro("Categoria não encontrada.");
                return;
            }
        }
        #endregion

    }
}

