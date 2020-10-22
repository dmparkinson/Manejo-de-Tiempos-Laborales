using ReglasNegocio;
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
            TipoAusenciaLN tAusencia = new TipoAusenciaLN();

            List<string> lista = tAusencia.ListarTipoAusencia();
            ViewBag.ListaCatalogoAusencias = lista;
            ViewBag.Respuesta = "";
            return View();
        }



        [HttpPost]
        public JsonResult  Insertar()
        {
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult  Editar()
        {
            return Json(new { success = false });
        }


        [HttpPost]
        public JsonResult  Eliminar()
        {
            return Json(new { success = false });
        }

    }
        
}
