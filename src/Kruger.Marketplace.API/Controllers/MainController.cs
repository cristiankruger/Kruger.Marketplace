using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Kruger.Marketplace.CrossCutting.App;
using Kruger.Marketplace.CrossCutting.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
                NotificateInvalidModelError(modelState);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            return !_notificador.TemNotificacao() ?
                                Ok(new { success = true, data = result }) :
                                BadRequest(new { success = false, errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem) });
        }

        protected void NotificateInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in errors)
            {
                var errorMsg = erro.Exception is null ? erro.ErrorMessage : erro.Exception.Message;

                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }
    }
}
