using Entidad;
using Newtonsoft.Json;
using ReglasNegocio;
using ReglasNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows;

namespace Presentacion.Controllers
{
    public class Historico_AusenciasController : Controller
    {
        // GET: Historico_Ausencias
        public ActionResult Listar_de_Admin()
        {
            AusenciaRN ausencias = new AusenciaRN();
            TipoAusenciaLN tAusencia = new TipoAusenciaLN();


            List<TipoAusencia> listaTipoA = JsonConvert.DeserializeObject<List<TipoAusencia>>(tAusencia.ListarTipoAusencia());
            List<Ausencia> lista = JsonConvert.DeserializeObject<List<Ausencia>>(ausencias.ListarHistoricoAusencias());
            ViewBag.ListaHistoricAusencias = lista;
            ViewBag.ListaTipoAusencias = listaTipoA;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Histórico de Ausencias";
            return View();
        }
        public ActionResult Listar_de_Jefatura()
        {

            AusenciaRN ausencias = new AusenciaRN();
            TipoAusenciaLN tAusencia = new TipoAusenciaLN();


            List<TipoAusencia> listaTipoA = JsonConvert.DeserializeObject<List<TipoAusencia>>(tAusencia.ListarTipoAusencia());
            List<Ausencia> lista = JsonConvert.DeserializeObject<List<Ausencia>>(ausencias.ListarHistoricoAusencias());
            ViewBag.ListaHistoricAusencias = lista;
            ViewBag.ListaTipoAusencias = listaTipoA;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Histórico de Ausencias";
            return View();
        }





        // EDIT: Historico_Ausencias
        [HttpPost]
        public JsonResult Editar(string idAusencia, string idUsuario, string tipoNuevo, string fechaSalidaNuevo, string fechaRegresoNuevo)
        {
            AusenciaRN ausenciaRN = new AusenciaRN();

            int respuesta = ausenciaRN.EditarAusencia( idAusencia, idUsuario, tipoNuevo, fechaSalidaNuevo, fechaRegresoNuevo);
            if (respuesta == 1) // la ausencia se modifico exitosamente 
            {
                return Json(new { success = true});
            }
            else  // La ausencia no se modifico 
            {
                return Json(new { success = false});
            }

        }




        // DELETE: Historico_Ausencias
        [HttpPost]
        public JsonResult Eliminar(string ausencia)
        {
            AusenciaRN ausenciaRN = new AusenciaRN();
            int respuesta = ausenciaRN.EliminarAusencia(ausencia);

            if (respuesta == 1) // El tipo de ausencia no se encuentra en el sistema
            {
                return Json(new { success = true, deleted = true, dato= ausencia });
            }
            else  // El tipo de ausencia si se encontro y se elimino exitosamente
            {
                return Json(new { success = false, deleted = false, dato = ausencia });

               
            }
 
        }

    }
}