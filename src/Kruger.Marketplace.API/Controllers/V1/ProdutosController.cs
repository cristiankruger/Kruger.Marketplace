using Asp.Versioning;
using AutoMapper;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.CrossCutting.App;
using Kruger.Marketplace.CrossCutting.Expressions;
using Kruger.Marketplace.CrossCutting.ViewModels.CadastroBasico.Produto;
using Kruger.Marketplace.CrossCutting.ViewModels.Pagina;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Kruger.Marketplace.API.Controllers.V1
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiversion}/[controller]")]
    public class ProdutosController(IProdutoService produtoService,
                                    IMapper mapper,
                                    IAppIdentityUser user,
                                    INotificador notificador) : MainController(notificador, user)
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProdutoService _produtoService = produtoService;

        #region READ
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProdutoFilter filter)
        {
            var predicate = _mapper.Map<Expression<Func<Produto, bool>>>(ControllerExpression.GetFilterExpression(filter));
            int totalRecords = await _produtoService.GetTotal(predicate);

            var paged = new PagedViewModel<ProdutoViewModel>
            {
                TotalRecords = totalRecords,
                PagedData = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoService.GetAll(predicate,
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
        public async Task<IActionResult> Post([FromForm] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _produtoService.Add(_mapper.Map<Produto>(produtoViewModel)))
                return CustomResponse();

            await SaveChanges();
            return CustomResponse(produtoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromForm] ProdutoViewModel produtoViewModel)
        {
            if (produtoViewModel is null || id != produtoViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo passado na query.");
                return CustomResponse(produtoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _produtoService.Update(_mapper.Map<Produto>(produtoViewModel)))
                return CustomResponse();

            await SaveChanges();
            return CustomResponse(produtoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _produtoService.Delete(id))
                return CustomResponse();

            await SaveChanges();
            return CustomResponse($"produto excluída.");
        }
        #endregion

        #region PRIVATE_METHODS       
        private async Task<ProdutoViewModel> Get(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoService.GetById(id));
        }

        private async Task SaveChanges()
        {
            await _produtoService.SaveChanges();
        }
        #endregion

    }
}

