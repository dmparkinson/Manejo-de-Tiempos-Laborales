using Entidad;
using Newtonsoft.Json;
using Presentacion.Security;
using ReglasNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Presentacion.Controllers
{
    public class AccountController : Controller
    {
        // GET: Init
        [NoLogin]
        public ActionResult Login()
        {
            ViewBag.Message = "Manejo de tiempos laborales.";

            return View();
        }




        [Authorize]
        public ActionResult Logout()
        {
            Security.SessionSecurity.DestroyUserSession();
            return RedirectToAction("Login");
        }




        // Se encarga del inicio de sesion
        [HttpPost]
        public JsonResult CheckUser(string name, string password, string ReturnURL)
        {

            // Llamado a negocio
            LoginLN login = new LoginLN();
            Empleado usuario = JsonConvert.DeserializeObject<Empleado>(login.LoginUsser(name, password));
            if (usuario != null) // Si el usuario existe
            {
                Security.SessionSecurity.AddUserToSession(usuario.TN_Id_Usuario.ToString());
                return Json(new { success = true, url = Url.Action("RedirHome") });
            }
            else
            {// Si el usuario no existe
                return Json(new { success = false });
            }
        }






        // Redirecciona a la pagina principal del tipo de usuario y guarda en variables de sesion
        [Authorize]
        public RedirectToRouteResult RedirHome()
        {
            // Solicitar el tipo de usuario
            EmpleadoRN empleadoRN = new EmpleadoRN();
            Empleado empleado = new Empleado();
            empleado.TN_Id_Usuario = Security.SessionSecurity.GetUser(); // Obtener el id del empleado logueado
            Empleado usuario = JsonConvert.DeserializeObject<Empleado>(empleadoRN.ObtenerEmpleado(empleado)); // Obtener info del empleado

            Session["UsserName"] = usuario.TC_Nombre_Usuario;
            Session["UsserSurname1"] = usuario.TC_Primer_Apellido;
            Session["UsserSurname2"] = usuario.TC_Segundo_Apellido;
            Session["UsserID"] = usuario.TN_Id_Usuario;
            Session["UsserPassword"] = usuario.TC_Contrasena;
            Session["UsserType"] = usuario.TC_Tipo_Usuario;

            if (usuario.TC_Tipo_Usuario == "Administración") {
                return RedirectToAction("Listar", "Historico_Tiempos_Laborales");
            }
            else if (usuario.TC_Tipo_Usuario == "Jefatura") {
                return RedirectToAction("Listar", "Historico_Tiempos_Laborales");
            }
            else if (usuario.TC_Tipo_Usuario == "Estándar") {
                return RedirectToAction("Listar_de_Empleado", "Empleado_Horarios");
            }
            else
            {
                Security.SessionSecurity.DestroyUserSession(); // Eliminarle la sesion al usuario malicioso
                return RedirectToAction("Error403", "Error");
            }
        }

    }

        
}
