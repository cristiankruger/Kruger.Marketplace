using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.Application.App;
using Kruger.Marketplace.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
