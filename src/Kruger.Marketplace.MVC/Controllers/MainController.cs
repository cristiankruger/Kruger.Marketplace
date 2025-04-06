﻿using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Business.Notificacoes;
using Kruger.Marketplace.CrossCutting.App;
using Microsoft.AspNetCore.Mvc;

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
    }
}
