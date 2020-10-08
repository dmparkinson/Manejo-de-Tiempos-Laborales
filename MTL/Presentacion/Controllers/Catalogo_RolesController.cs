using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_RolesController : Controller
    {
        // GET: Catalogo_Rol
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Roles";
            return View();
        }
    }
}
