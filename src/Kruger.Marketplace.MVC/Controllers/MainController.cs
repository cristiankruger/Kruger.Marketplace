using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace Kruger.Marketplace.MVC.Controllers
{
    public abstract class MainController(INotificador notificador) : Controller
    {
        private readonly INotificador _notificador = notificador;        

        protected void GetErrorsFromNotificador()
        {
            if (!_notificador.TemNotificacao())
                return;

            foreach (var error in _notificador.ObterNotificacoes())
                if (!ModelState.Values.SelectMany(e => e.Errors).Any(e => e.ErrorMessage == error.Mensagem))
                    ModelState.AddModelError(string.Empty, error.Mensagem);
        }

        protected void NotificarErro(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }
    }
}
