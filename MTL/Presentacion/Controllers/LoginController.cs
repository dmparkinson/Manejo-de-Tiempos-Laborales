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



            if (name == "administrador" || name == "jefatura" || name == "estandar") // Si el usuario existe
            {

                Session["usserName"] = name;
                Session["usserType"] = name;

                if (Session["usserType"].ToString() == "administrador")
                {
                    return Json(new { success = true, url = Url.Action("Listar_de_Admin", "Historico_Horarios") });
                }
                else if (Session["usserType"].ToString() == "jefatura")
                {
                    return Json(new { success = true, url = Url.Action("Listar_de_Jefatura", "Historico_Horarios") });
                }
                else
                {
                    return Json(new { success = true, url = Url.Action("Listar_de_Empleado", "Empleado_Horarios") });

                }
            }
            else   // Si el usuario no existe
            {
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
