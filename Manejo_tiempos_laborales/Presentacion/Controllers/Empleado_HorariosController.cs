using Entidad;
using ReglasNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesoDatos.Implementaciones;
using Newtonsoft.Json;
using Presentacion.Security;

namespace Presentacion.Controllers
{
    [Autenticado]
    public class Empleado_HorariosController : Controller
    {
        // GET: Empleados_Horarios
        public ActionResult Listar_de_Admin()
        {
            //debo listar el catalogo de tiempos
            string listaH = new HorarioAD().listarHorarios();
            List<Horario> lista = JsonConvert.DeserializeObject<List<Horario>>(listaH);

            string lisT = new TiemposAD().consultarTiemposUsuario(int.Parse(Session["UserIdAdminMarcas"].ToString()));
            List<Tiempo> listaT = JsonConvert.DeserializeObject<List<Tiempo>>(lisT);

            string sEmp = new EmpleadoAD().consultarEmpleado(int.Parse(Session["UserIdAdminMarcas"].ToString()));
            Empleado emp = JsonConvert.DeserializeObject<Empleado>(sEmp);

            ViewBag.listaHorario = lista;
            ViewBag.listaTiempo = listaT;
            ViewBag.Message = emp.TC_Nombre_Usuario + " " + emp.TC_Primer_Apellido + " " + emp.TC_Segundo_Apellido;
            return View();
        }

        public JsonResult seleccionAdminEmpleadoMarcas(int idEmpleado) {
            Session["UserIdAdminMarcas"] = idEmpleado;
            return Json(new { success = true, url = Url.Action("Listar_de_Admin", "Empleado_Horarios") });
        }


        public ActionResult Listar_de_Jefatura()
        {

            ViewBag.Message = "Horarios de Empleado";
            return View();
        }


        public ActionResult Listar_de_Empleado()
        {
            //debo listar el catalogo de tiempos
            List<Horario> lista = new List<Horario>();
            string listaH = new HorarioAD().listarHorarios();
            lista = JsonConvert.DeserializeObject<List<Horario>>(listaH);

            List<Tiempo> listaT = new List<Tiempo>();
            string lisT = new TiemposAD().listarPorFechaTiempoUsuario(int.Parse(Session["UsserID"].ToString()), DateTime.Now.ToString("dd-MM-yyyy"));
            listaT = JsonConvert.DeserializeObject<List<Tiempo>>(lisT);

            ViewBag.listaHorario = lista;
            ViewBag.listaTiempo = listaT;
            ViewBag.Message = Session["UsserName"].ToString() +" "+ Session["UsserSurname1"].ToString() + " " + Session["UsserSurname2"].ToString();
            return View();
        }


        //registramos el tiempo de un empleado
        public int registrarTiemposEmpleado(int idTiempo, string tiempo) {
            char[] tipo = tiempo.ToCharArray();
            Tiempo t = new Tiempo();
            t.TN_Id_Horario = idTiempo;
            t.TC_Horario = tiempo;
            //t.horario.TC_Horario= tiempo;
            t.TC_Tipo = tipo[0].ToString();
            t.TN_Id_Usuario = int.Parse(Session["UsserID"].ToString());

            //acá hay que aplicar las reglas de negocio
            TiempoRN tiempoRN = new TiempoRN();

            int res = tiempoRN.verificarRegistro(tiempo, t.TN_Id_Usuario);
            if (res == 1) {
                return new TiemposAD().registrarTiempo(t);
            }

            //si las reglas de negocio dan el aval para registrar
            //se llama al acceso de datos
            return res;
        }


        //refrescamos las marcas de tiempo del usuario
        public string refrescarMarcasTiempo() {
            List<Tiempo> listaT = new List<Tiempo>();
            return new TiemposAD().listarPorFechaTiempoUsuario(int.Parse(Session["UsserID"].ToString()), DateTime.Now.ToString("dd-MM-yyyy"));
        }


        // GET: Empleados_Horarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empleados_Horarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados_Horarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empleados_Horarios/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empleados_Horarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Empleados_Horarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empleados_Horarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
