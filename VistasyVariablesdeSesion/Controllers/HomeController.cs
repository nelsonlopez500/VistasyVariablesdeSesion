using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VistasyVariablesdeSesion.Models;
using VistasyVariablesdeSesion.Servicios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VistasyVariablesdeSesion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Autenticacion]
        public IActionResult Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var tipoUsuario = HttpContext.Session.GetString("TipoUsuario");
            var nombreUsuario = HttpContext.Session.GetString("Nombre");

            if (usuarioId == null)
            {
                return RedirectToAction("Autenticar");
            }

            ViewBag.nombre = nombreUsuario;
            ViewData["tipoUsuario"] = tipoUsuario;

            return View();
        }

        public IActionResult Autenticar()
        {
            ViewData["ErrorMessage"] = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(string txtUsuario, string txtClave)
        {
            // Valido al usuario con la base de datos
            var usuario = (from u in _context.usuarios
                           where u.email == txtUsuario
                           && u.contrasenia == txtClave
                           && u.activo == "S"
                           && u.bloqueado == "N"
                           select u).FirstOrDefault();

            // Si el usuario existe con todas sus validaciones
            if (usuario != null)
            {
                // Se crean las variables de sesion
                HttpContext.Session.SetInt32("UsuarioId", usuario.id_usuario);
                HttpContext.Session.SetString("TipoUsuario", usuario.tipo_usuario);
                HttpContext.Session.SetString("Nombre", usuario.nombre);

                // Se redirecciona al metodo Index del controlador Home
                return RedirectToAction("Index", "Home");
            }

            // Muestra por ViewData un error
            ViewData["ErrorMessage"] = "Error, usuario inválido!";
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Autenticar");
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