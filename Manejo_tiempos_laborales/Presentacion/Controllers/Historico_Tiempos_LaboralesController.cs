using AccesoDatos.Implementaciones;
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

    [Authorize]
    public class Historico_Tiempos_LaboralesController : Controller
    {
        // GET: Historicos_Tiempos

        public ActionResult Listar()
        {
            TiempoRN tiempoUs = new TiempoRN();

            List<Tiempo> lista = JsonConvert.DeserializeObject<List<Tiempo>>(tiempoUs.ListarHistoricoTiempos());
            List<Horario> listaH = JsonConvert.DeserializeObject<List<Horario>>(new HorarioAD().listarHorarios());

            ViewBag.Message = "Histórico de Tiempos Laborales";
            ViewBag.ListaHistoricoTiempos = lista;
            ViewBag.ListaHorarios = listaH;
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
                return RedirectToAction("Error403", "Error");
            }
        }



        [HttpPost]
        public int Editar(int idT, int idU, int idH, string fecha, string hora, string tiempo)
        {
            Tiempo temp = new Tiempo();
            temp.TN_Id_Tiempo = idT;
            temp.TN_Id_Usuario = idU;
            temp.TN_Id_Horario = idH;
            temp.TF_Fecha = fecha;
            temp.TH_Hora = hora;
            temp.TC_Horario = tiempo;
            temp.TC_Tipo = tiempo.ToCharArray()[0].ToString();

            //debemos consultar que el tiempo exista
            Tiempo consulta = JsonConvert.DeserializeObject<Tiempo>(new TiempoRN().consultarTiempoHistoricoUsuarioRN(temp));
            if (consulta.TC_Horario == null || consulta.TC_Horario.Equals("")){
                return 0;
            }
            else {
                if (consulta.TN_Id_Tiempo == idT){
                    return new TiempoRN().actualizarTiempoHistoricoUsuarioRN(temp);
                    //return idH;
                }
                else {
                    return 0;
                }
            }
        }

        [HttpPost]
        public int Eliminar(int idTiempo)
        {
            return new TiempoRN().eliminarTiempoHistoricoUsuarioRN(idTiempo);
        }

        public string RefrescarTablaHistorico() {
            return new TiempoRN().ListarHistoricoTiempos();
        }
    }
}
