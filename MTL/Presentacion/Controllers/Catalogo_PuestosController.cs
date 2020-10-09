using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_PuestosController : Controller
    {
        // GET: Catalogo_Puestos
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Puestos";
            return View();
        }
    }
}
