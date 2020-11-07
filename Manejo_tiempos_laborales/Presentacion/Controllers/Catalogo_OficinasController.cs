using Newtonsoft.Json;
using Presentacion.Security;
using ReglasNegocio;
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
        [Authorize]
        [Autenticado]
        public ActionResult Listar()
        {
            OficinaRN o = new OficinaRN();
            ViewBag.Lista = o.listarOficinas("");
            ViewBag.Circuitos = o.listarCircuitosOficinas();
            ViewBag.Message = "Catálogo de Oficinas";
            if (Session["UsserType"].ToString() == "Administración")
            {
                return View("Listar");
            }
            else
            {
                return View("Error");
            }
        }

        public JsonResult Insertar(string codigo, string nombre, int circuito, int activo, string fechai, string fechaf)
        {
            OficinaRN rn = new OficinaRN();
            int respuesta = rn.insertarOficina(codigo,nombre,circuito,activo,fechai,fechaf);

            if (respuesta == 1)
            {
                return Json(new { success = true, inserted = true });
            }
            else
            {
                if (respuesta == 0)
                {
                    return Json(new { success = true, inserted = false });
                }
                else
                {
                    return Json(new { success = false, inserted = false });
                }
            }
        }

        public JsonResult Actualizar(int id, string codigo, string nombre, int circuito, int activo, string fechai, string fechaf)
        {
            OficinaRN rn = new OficinaRN();
            int respuesta = rn.actualizarOficina(id ,codigo, nombre, circuito, activo, fechai, fechaf);

            if (respuesta == 1)
            {
                return Json(new { success = true, updated = true });
            }
            else
            {
                if (respuesta == 0)
                {
                    return Json(new { success = true, updated = false });
                }
                else
                {
                    return Json(new { success = false, updated = false });
                }
            }
        }

        public JsonResult Eliminar(int id)
        {
            OficinaRN rn = new OficinaRN();
            int respuesta = rn.eliminarOficina(id);

            if (respuesta == 1)
            {
                return Json(new { success = true, deleted = true });
            }
            else
            {
                if (respuesta == 0)
                {
                    return Json(new { success = false, deleted = false });
                }
                else
                {
                    return Json(new { success = true, deleted = false });
                }
            }
        }

        public string Refrescar()
        {
            OficinaRN o = new OficinaRN();
            return JsonConvert.SerializeObject(o.listarOficinas(""));
        }

    }
}
