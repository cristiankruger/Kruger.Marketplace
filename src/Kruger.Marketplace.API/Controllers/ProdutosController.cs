using AutoMapper;
using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Interfaces.Services.CadastroBasico;
using Kruger.Marketplace.Business.Models.CadastroBasico;
using Kruger.Marketplace.Business.Models.Settings;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.Expressions;
using Kruger.Marketplace.Application.ViewModels.CadastroBasico.Produto;
using Kruger.Marketplace.Application.ViewModels.Pagina;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Linq.Expressions;
using System.Net;

namespace Kruger.Marketplace.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProdutosController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;
        private readonly ArquivoSettings _arquivoSettings;

        public ProdutosController(IProdutoService produtoService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IAppIdentityUser user,
                                  IOptions<ArquivoSettings> arquivoSettings) : base(notificador, user)
        {
            _mapper = mapper;
            _produtoService = produtoService;
            _arquivoSettings = arquivoSettings.Value;        
        }


        #region READ
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] ProdutoFilter filter)
        {
            filter.SetVendedorId(UserId);
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
            
            return CustomResponse(HttpStatusCode.OK, paged);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var produtoViewModel = await Get(id);

            if (produtoViewModel is not null)
                return CustomResponse(HttpStatusCode.OK, produtoViewModel);

            NotificarErro("Produto não encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }
        #endregion

        #region WRITE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromForm] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(produtoViewModel.Imagem);

            if (!await _produtoService.Add(_mapper.Map<Produto>(produtoViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);
            
            await SaveChanges(produtoViewModel.Id);

            return CustomResponse(HttpStatusCode.Created, null, produtoViewModel, produtoViewModel.Id);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromForm] ProdutoViewModel produtoViewModel)
        {
            if (produtoViewModel is null || id != produtoViewModel.Id)
            {
                NotificarErro("O Id informado não é o mesmo passado na query.");
                return CustomResponse(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            produtoViewModel.SetVendedorId(UserId);
            produtoViewModel.SetImageProperties(produtoViewModel.Imagem);

            if (!await _produtoService.Update(_mapper.Map<Produto>(produtoViewModel)))
                return CustomResponse(HttpStatusCode.BadRequest);

            await SaveChanges(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _produtoService.Delete(id))
                return CustomResponse(HttpStatusCode.BadRequest);

            await SaveChanges(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
        #endregion

        #region PRIVATE_METHODS       
        private async Task<ProdutoViewModel> Get(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoService.GetById(id));
        }

        private async Task SaveChanges(Guid id)
        {
            try
            {
                await _produtoService.SaveChanges();
            }
            catch (DBConcurrencyException)
            {
                if (await Get(id) is not null)
                    throw;

                NotificarErro("Produto não encontrado.");
                return;
            }
        }
        #endregion

    }
}

