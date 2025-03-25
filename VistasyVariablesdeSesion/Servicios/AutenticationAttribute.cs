using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VistasyVariablesdeSesion.Servicios
{
    public class AutenticacionAttribute : ActionFilterAttribute
    {
        // Este método se ejecuta antes de que se ejecute la acción del controlador
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Verificar si la variable de sesión 'UsuarioId' existe
            var usuarioId = context.HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
            {
                // Si no está autenticado, redirigir a la página de login
                context.Result = new RedirectToActionResult("Autenticar", "Home", null);
            }

            base.OnActionExecuting(context);
        }
    }
}