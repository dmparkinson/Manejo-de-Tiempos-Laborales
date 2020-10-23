using Entidad;
using Newtonsoft.Json;
using ReglasNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Presentacion.Controllers
{
    public class LoginController : Controller
    {
        // GET: Init
        public ActionResult Index()
        {
            ViewBag.Message = "Manejo de tiempos laborales.";

            return View();
        }




        [HttpPost]
        public JsonResult LogIn(string name, string password)
        {

            // Llamado a negocio

            LoginLN login = new LoginLN();

            Empleado empleado =  JsonConvert.DeserializeObject<Empleado>(login.LoginUsser(name, password));


            if (empleado.TC_Tipo_Usuario == "Administración" || empleado.TC_Tipo_Usuario == "Jefatura" || empleado.TC_Tipo_Usuario == "Estándar") // Si el usuario existe
            {

                Session["UsserName"] = empleado.TC_Nombre_Usuario;
                Session["UsserSurname1"] = empleado.TC_Primer_Apellido;
                Session["UsserSurname2"] = empleado.TC_Segundo_Apellido;
                Session["UsserID"] = empleado.TN_Id_Usuario;
                Session["UsserPassword"] = empleado.TC_Contrasena;
                Session["UsserType"] = empleado.TC_Tipo_Usuario;

                if (Session["UsserType"].ToString() == "Administración")
                {
                    return Json(new { success = true, url = Url.Action("Listar_de_Admin", "Historico_Horarios") });
                }
                else if (Session["usserType"].ToString() == "Jefatura")
                {
                    return Json(new { success = true, url = Url.Action("Listar_de_Jefatura", "Historico_Horarios") });
                }
                else
                {
                    return Json(new { success = true, url = Url.Action("Listar_de_Empleado", "Empleado_Horarios") });

                }


            }
            else
            {// Si el usuario no existe
                return Json(new { success = false });
            }




        }




        public ActionResult Logout()
        {
           
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Login");

        }



    }

        
}
