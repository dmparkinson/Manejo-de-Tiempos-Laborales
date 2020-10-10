using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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



        public ActionResult Sesion()
        {

            Session["usser"] = 1;
            int num = (int)Session["usser"];

            if (num  == 1)
            {
                return RedirectToAction("Listar", "Historico_Horarios");
            }
            else if (num == 2)
            {
                return RedirectToAction("Listar","Historico_Horarios");
            }
            else {
                return RedirectToAction("Listar", "Empleado_Horarios");
            }

        }



    }

        
}
