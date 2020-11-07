using AccesoDatos.Implementaciones;
using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_HorariosController : Controller
    {
        public ActionResult Agregar(String horario) {
            Horario t = new Horario { TC_Horario = horario, TH_Duracion = "00:00:00" };

            //primero debemos confirmar si el tiempo ya existe
            Horario temp = new HorarioAD().consultarTiempo(t);
            if (temp.TC_Horario == null){
                int res = new HorarioAD().registrarHorario(t);
            }
            else {
                if (temp.TC_Horario.Equals(t.TC_Horario) && temp.TH_Duracion.Equals(t.TH_Duracion)){
                    ViewBag.Respuesta = "El registro de tiempo ya existe";
                }else{
                    int res = new HorarioAD().registrarHorario(t);
                }
            }


            ViewBag.Message = "Catálogo de Horarios";
            string listaS = new HorarioAD().listarHorarios();
            List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaS);
            ViewBag.ListaTiempos = lista;
            return View("Listar");
        }

        public int Eliminar(String horario) {
            Horario temp = new HorarioAD().consultarTiempo(new Horario { TC_Horario = horario, TH_Duracion = "00:00:00" });
            if (temp.TC_Horario == null){
                ViewBag.Respuesta = "El registro '" + horario + "' no existe";
                return 0;
            }else{
                Horario t = new Horario();
                t.TC_Horario = horario;
                t.TH_Duracion = "00:00:00";
                ViewBag.Respuesta = "El registro se eliminó correctamente";
                return new HorarioAD().eliminarHorario(t);
            }
        }


        public ActionResult Actualizar(String viejo, String nuevo) {

            //confirmar que existe un registro con el nombre viejo
            Horario temp = new HorarioAD().consultarTiempo(new Horario {TC_Horario = viejo, TH_Duracion= "00:00:00" });
            if (temp.TC_Horario == null){
                ViewBag.Respuesta = "El registro '"+viejo+"' no existe";
            }
            else {
                Horario t = new Horario();
                t.TC_Horario = nuevo;
                t.TH_Duracion = "00:00:00";
                int res = new HorarioAD().actualizarHorario(t);
                ViewBag.Respuesta = "El registro se actualizó correctamente";
            }

            ViewBag.Message = "Catálogo de Horarios";
            string listaS = new HorarioAD().listarHorarios();
            List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaS);
            ViewBag.ListaTiempos = lista;
            ViewBag.Respuesta = "Actualizado";
            return View("Listar");
        }

        // GET: Catalogo_Tiempos_Laborales
        public ActionResult Listar()
        {
            ViewBag.Message = "Catálogo de Horarios";
            string listaS = new HorarioAD().listarHorarios();
            List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaS);
            ViewBag.ListaTiempos = lista;
            ViewBag.Respuesta = "Actualizado";

            if (Session["UsserType"].ToString() == "Administración")
            {
                return View("Listar");
            }
            else
            {
                return View("Error");
            }

        }

        
    }
}
