using Microsoft.SqlServer.Server;
using ReglasNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        public ActionResult Listar_de_Admin()
        {
            EmpleadoRN e = new EmpleadoRN();
            ViewBag.Oficinas = e.listarOficinasEmpleado();
            ViewBag.Puestos = e.listarPuestosEmpleado();
            ViewBag.Lista = e.ListarEmpleados();
            ViewBag.Message = "Empleados";
            return View();
        }
        public ActionResult Listar_de_Jefatura()
        {
            ViewBag.Message = "Empleados";
            return View();
        }

        public JsonResult Insertar(string usuario, string password, string cedula, string nombre, string apUno, string apDos, string correo, string tipo, string estado, int puesto, int oficina)
        {
            EmpleadoRN e = new EmpleadoRN();
            int respuesta = e.Insertar(usuario,password,cedula,nombre,apUno,apDos,correo,tipo,estado,puesto,oficina);

            if (respuesta == 1) // El tipo de ausencia se agregoexitosamente 
            {
                return Json(new { success = true });
            }
            else  // El tipo de ausencia no se registro en el sistema
            {
                return Json(new { success = false });
            }
        }

        public JsonResult Eliminar(string cedula)
        {
            EmpleadoRN e = new EmpleadoRN();
            int respuesta = e.Eliminar(cedula);
            if (respuesta == 0) // El tipo de ausencia no se encuentra en el sistema
            {
                return Json(new { success = false, deleted = false });
            }
            if (respuesta == 1)  // El tipo de ausencia si se encontro y se elimino exitosamente
            {
                return Json(new { success = true, deleted = true });
            }               
            else
            {
                return Json(new { success = true, deleted = false });
            }
        }


        // GET: Empleados/Details/5
        public ActionResult Detallar(int id)
        {
            return View();
        }

        // GET: Empleados/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Empleados/Create
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

        // GET: Empleados/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empleados/Edit/5
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

        // GET: Empleados/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empleados/Delete/5
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
