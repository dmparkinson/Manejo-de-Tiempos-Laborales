using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_CircuitosController : Controller
    {
        // GET: Catalogo_Circuitos
        public ActionResult Listar()
        {

            ViewBag.Message = "Catálogo de Circuitos";
            return View();
        }

    }
}
