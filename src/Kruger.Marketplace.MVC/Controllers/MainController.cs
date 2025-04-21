using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kruger.Marketplace.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        private readonly INotificador _notificador;
        public readonly IAppIdentityUser _user;
        protected Guid UserId { get; set; }
        protected string UserName { get; set; }

        protected MainController(INotificador notificador, IAppIdentityUser user)
        {
            _notificador = notificador;
            _user = user;

            if (user.IsAuthenticated())
            {
                //TODO: Trocar para o vendedor logado
                //Hardcode (FK do unico vendedor no arquivo seedDatabase)
                UserId = user.GetUserId();
                UserName = user.GetUsername();
            }
        }

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

        protected List<SelectListItem> GetPageSizeList(int selected)
        {
            var pagedItemsList = new List<SelectListItem>() {
                new() { Value = "10", Text = "10", Selected = false},
                new() { Value = "25", Text = "25", Selected = false},
                new() { Value = "50", Text = "50" , Selected = false},
                new() { Value = "100", Text = "100" , Selected = false}
            };

            foreach (var item in pagedItemsList)
                if (item.Value == selected.ToString())
                    item.Selected = true;

            return pagedItemsList;
        }
    }
}
