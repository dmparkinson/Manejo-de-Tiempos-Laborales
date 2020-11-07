using Entidad;
using Newtonsoft.Json;
using ReglasNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Historico_HorariosController : Controller
    {
        // GET: Historicos_Horarios
        public ActionResult Listar_de_Admin()
        {
            TiempoRN tiempoUs = new TiempoRN();

            List<Tiempo> lista = JsonConvert.DeserializeObject<List<Tiempo>>(tiempoUs.ListarHistoricoTiempos());
            ViewBag.Message = "Histórico de Horarios";
            ViewBag.ListaHistoricoHorarios = lista;
            ViewBag.Respuesta = "";
            return View();
        }

        public ActionResult Listar_de_Jefatura()
        {
            TiempoRN tiempoUs = new TiempoRN();

            List<Tiempo> lista = JsonConvert.DeserializeObject<List<Tiempo>>(tiempoUs.ListarHistoricoTiempos());
            ViewBag.Message = "Histórico de Horarios";
            ViewBag.ListaHistoricoHorarios = lista;
            ViewBag.Respuesta = "";
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
