using AccesoDatos.Implementaciones;
using Entidad;
using Newtonsoft.Json;
using Presentacion.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    [Autenticado]
    public class Catalogo_HorariosController : Controller
    {
        public int Agregar(String horario) {
            Horario t = new Horario();
            t.TC_Horario = horario;
            t.TH_Duracion = "00:00:00";

            //primero debemos confirmar si el tiempo ya existe
            Horario temp = new HorarioAD().consultarTiempo(t);
            if (temp.TC_Horario == null){
                return new HorarioAD().registrarHorario(t);
            }
            else {
                if (temp.TC_Horario.Equals(t.TC_Horario) && temp.TH_Duracion.Equals(t.TH_Duracion)){
                    ViewBag.Respuesta = "El registro de tiempo ya existe";
                    return 0;
                }else{
                    return new HorarioAD().registrarHorario(t);
                }
            }

            //ViewBag.Message = "Catálogo de Horarios";
            //string listaS = new HorarioAD().listarHorarios();
            //List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaS);
            //ViewBag.ListaTiempos = lista;
            //return View("Listar");
        }

        public int Eliminar(string horario) {
            Horario temp = new HorarioAD().consultarTiempo(new Horario { TC_Horario = horario, TH_Duracion = "00:00:00" });
            if (temp.TC_Horario == null){
                return 0;
            }else{
                return new HorarioAD().eliminarHorario(new Horario { TC_Horario = horario, TH_Duracion = "00:00:00" });
            }
        }


        public int Actualizar(string viejo, string nuevo) {

            Horario vH = new Horario();
            vH.TC_Horario = viejo;
            vH.TH_Duracion = "00:00:00";

            Horario nH = new Horario();
            nH.TC_Horario = nuevo;
            nH.TH_Duracion = "00:00:00";

            //confirmar que existe un registro con el nombre viejo
            Horario temp = new HorarioAD().consultarTiempo(vH);
            if (temp.TC_Horario == null){
                return 0;
            }
            else {
                return new HorarioAD().actualizarHorario(viejo, nH);
            }

            //ViewBag.Message = "Catálogo de Horarios";
            //string listaS = new HorarioAD().listarHorarios();
            //List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaS);
            //ViewBag.ListaTiempos = lista;
            //ViewBag.Respuesta = "Actualizado";
            //return View("Listar");
        }

        // GET: Catalogo_Tiempos_Laborales
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Horarios";
            string listaS = new HorarioAD().listarHorarios();
            List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaS);
            ViewBag.ListaTiempos = lista;
            ViewBag.Respuesta = "";

            if (Session["UsserType"].ToString() == "Administración")
            {
                return View("Listar");
            }
            else
            {
                return RedirectToAction("Error403", "Error");
            }

        }

        public string RefrescarTablaCatalogo() {
            return new HorarioAD().listarHorarios();
        }

    }
}
