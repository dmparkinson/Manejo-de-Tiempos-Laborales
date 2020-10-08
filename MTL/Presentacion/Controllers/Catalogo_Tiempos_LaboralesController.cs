using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_Tiempos_LaboralesController : Controller
    {
        // GET: Catalogo_Tiempos_Laborales
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Tiempos Laborales";
            return View();
        }

        
    }
}
