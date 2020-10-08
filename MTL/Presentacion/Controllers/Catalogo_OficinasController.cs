using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_OficinasController : Controller
    {
        // GET: Catalogo_Oficinas
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Oficinas";
            return View();
        }

    }
}
