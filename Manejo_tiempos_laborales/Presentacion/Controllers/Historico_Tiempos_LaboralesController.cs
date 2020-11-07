using Entidad;
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
    
    public class Historico_Tiempos_LaboralesController : Controller
    {
        // GET: Historicos_Tiempos
        [Authorize]
        [Autenticado]
        public ActionResult Listar()
        {
            TiempoRN tiempoUs = new TiempoRN();

            List<Tiempo> lista = JsonConvert.DeserializeObject<List<Tiempo>>(tiempoUs.ListarHistoricoTiempos());
            ViewBag.Message = "Histórico de Tiempos Laborales";
            ViewBag.ListaHistoricoTiempos = lista;
            ViewBag.Respuesta = "";
            if (Session["UsserType"].ToString() == "Administración")
            {
                return View("Listar_de_Admin");
            }
            else if (Session["UsserType"].ToString() == "Jefatura")
            {
                return View("Listar_de_Jefatura");
            }
            else {
                return View("Error");
            }
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
