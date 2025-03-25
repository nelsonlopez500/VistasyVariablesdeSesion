using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VistasyVariablesdeSesion.Models;

namespace VistasyVariablesdeSesion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");
            if (usuarioId == null)
            {
                return RedirectToAction("Autenticar");
            }

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
