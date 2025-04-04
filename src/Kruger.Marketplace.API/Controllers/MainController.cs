using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Kruger.Marketplace.API.Controllers
{
    [ApiController]
    public class MainController(INotificador notificador) : ControllerBase
    {
        protected const int defaultPageNumber = 1;
        protected const int defaultPageSize = 10;
        private readonly INotificador _notificador = notificador;

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
