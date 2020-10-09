using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_AusenciasController : Controller
    {
        // GET: Catalogo_Ausencias
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Ausencias";
            return View();
        }
    }
        
}
