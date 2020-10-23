using Entidad;
using AccesosDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccesosDatos.Implementaciones;
using Newtonsoft.Json;

namespace Presentacion.Controllers
{
    public class Empleado_HorariosController : Controller
    {
        // GET: Empleados_Horarios
        public ActionResult Listar_de_Admin()
        {

            ViewBag.Message = "Horarios de Empleado";
            return View();
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


            ViewBag.listaHorario = lista;
            ViewBag.Message = "Horarios de Empleado";
            return View();
        }


        //registramos el tiempo de un empleado
        public void registrarTiemposEmpleado(string fecha, string hora, string tiempo) {
            char[] tipo = tiempo.ToCharArray();


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
