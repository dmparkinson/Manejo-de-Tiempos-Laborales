using Entidades;
using Newtonsoft.Json;
using ReglasNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Historico_AusenciasController : Controller
    {
        // GET: Historico_Ausencias
        public ActionResult Listar_de_Admin()
        {
            AusenciaRN ausencias = new AusenciaRN();

            List<Ausencia> lista = JsonConvert.DeserializeObject<List<Ausencia>>(ausencias.ListarHistoricoAusencias());
            ViewBag.ListaHistoricAusencias = lista;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Histórico de Ausencias";
            return View();
        }
        public ActionResult Listar_de_Jefatura()
        {

            AusenciaRN ausencias = new AusenciaRN();

            List<Ausencia> lista = JsonConvert.DeserializeObject<List<Ausencia>>(ausencias.ListarHistoricoAusencias());
            ViewBag.ListaHistoricAusencias = lista;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Histórico de Ausencias";
            return View();
        }


        [HttpPost]
        public JsonResult Editar()
        {

            return null;

        }





        [HttpPost]
        public JsonResult Eliminar(string _ausencia)
        {
            AusenciaRN ausenciaRN = new AusenciaRN();
            int respuesta = ausenciaRN.EliminarAusencia(_ausencia);

            if (respuesta == 1) // El tipo de ausencia no se encuentra en el sistema
            {
                return Json(new { success = true, deleted = true });
            }
            else  // El tipo de ausencia si se encontro y se elimino exitosamente
            {
                return Json(new { success = false, deleted = false });

               
            }
 
        }

    }
}