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
            AusenciaLN ausencias = new AusenciaLN();

            List<Ausencia> lista = JsonConvert.DeserializeObject<List<Ausencia>>(ausencias.ListarHistoricoAusencias());
            ViewBag.ListaHistoricAusencias = lista;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Histórico de Ausencias";
            return View();
        }
        public ActionResult Listar_de_Jefatura()
        {

            AusenciaLN ausencias = new AusenciaLN();

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
        public JsonResult Eliminar()
        {
            return null;
        }

    }
}