using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Kruger.Marketplace.API.Controllers
{
    [ApiController]
    [AllowSynchronousIO]
    public abstract class MainController : ControllerBase
    {
        protected const int defaultPageNumber = 1;
        protected const int defaultPageSize = 10;

        private readonly INotificador _notificador;
        public readonly IAppIdentityUser _user;
        protected Guid UserId { get; set; }
        protected string UserName { get; set; }

        protected MainController(INotificador notificador,
                                 IAppIdentityUser user)
        {
            _notificador = notificador;
            
            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
                UserName = user.GetUsername();
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotificarInvalidModelStateError(modelState);

            return CustomResponse(HttpStatusCode.BadRequest);
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode, object result = null, object createdObj = null, object createdObjId = null)
        {
            return statusCode switch
            {
                HttpStatusCode.OK => Ok(new { success = true, data = result }),
                HttpStatusCode.Created => CreatedAtAction("GetById", new { success = true, id = createdObjId }, createdObj),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(new { success = false, errors = _notificador.TemNotificacao() ? _notificador.ObterNotificacoes().Select(n => n.Mensagem) : result }),
                HttpStatusCode.BadRequest => BadRequest(new { success = false, errors = _notificador.TemNotificacao() ? _notificador.ObterNotificacoes().Select(n => n.Mensagem) : result }),
                _ => throw new NotImplementedException($"Status code {statusCode} não implementado."),
            };
        }

        protected void NotificarInvalidModelStateError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in errors)
            {
                var errorMsg = erro.Exception is null ? erro.ErrorMessage : erro.Exception.Message;

                NotificarErro(errorMsg);
            }
        }

        protected void NotificarInvalidModelStateError(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            NotificarInvalidModelStateError(ModelState);
        }

        protected void NotificarErro(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }
    }
}
