using Kruger.Marketplace.Business.Interfaces.Notificador;
using Kruger.Marketplace.CrossCutting.App;
using Kruger.Marketplace.CrossCutting.ViewModels;
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
