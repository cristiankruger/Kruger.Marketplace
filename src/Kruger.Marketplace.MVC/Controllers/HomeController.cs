using Kruger.Marketplace.Core.Business.Interfaces.Notificador;
using Kruger.Marketplace.Core.Application.App;
using Kruger.Marketplace.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Kruger.Marketplace.MVC.Models;

namespace Kruger.Marketplace.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger,
                                INotificador notificador,
                                IAppIdentityUser user) : MainController(notificador, user)
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult Index()
        {
            return RedirectToAction("Index","Produtos");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A p�gina que est� procurando n�o existe! <br />Em caso de d�vidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! P�gina n�o encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Voc� n�o tem permiss�o para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);
        }
    }
}
