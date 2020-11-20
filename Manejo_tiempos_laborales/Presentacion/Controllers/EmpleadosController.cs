using Entidad;
using Microsoft.SqlServer.Server;
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
    [Autenticado]
    public class EmpleadosController : Controller
    {
        // GET: Empleados

        public ActionResult Listar()
        {
            EmpleadoRN e = new EmpleadoRN();
            ViewBag.Oficinas = e.listarOficinasEmpleado();
            ViewBag.Puestos = e.listarPuestosEmpleado();
            ViewBag.Lista = e.ListarEmpleados();
            ViewBag.Message = "Empleados";
            if (Session["UsserType"].ToString() == "Administración")
            {
                return View("Listar_de_Admin");
            }
            else if (Session["UsserType"].ToString() == "Jefatura")
            {
                return View("Listar_de_Jefatura");
            }
            else
            {
                return RedirectToAction("Error403", "Error");
            }
        }


        public string Refrescar()
        {
            EmpleadoRN e = new EmpleadoRN();
            return JsonConvert.SerializeObject(e.ListarEmpleados());
        }

        public JsonResult Insertar(string usuario, string password, string cedula, string nombre, string apUno, string apDos, string correo, string tipo, string estado, int puesto, int oficina)
        {
            EmpleadoRN e = new EmpleadoRN();
            int respuesta = e.Insertar(usuario,password,cedula,nombre,apUno,apDos,correo,tipo,estado,puesto,oficina);

            if (respuesta == 1) // El tipo de ausencia se agregoexitosamente 
            {
                return Json(new { success = true, inserted = true });
            }
            else  // El tipo de ausencia no se registro en el sistema
            {
                if (respuesta == 0)
                {
                    return Json(new { success = true, inserted = false });
                }
                else
                {
                    return Json(new { success = false, inserted = false });
                }
                
            }
        }

        public JsonResult Eliminar(int id)
        {
            EmpleadoRN e = new EmpleadoRN();
            int respuesta = e.Eliminar(id);
            if (respuesta == 0) // El empleado no se encuentra en el sistema
            {
                return Json(new { success = false, deleted = false });
            }
            else
            {
                if (respuesta == 1)  // El empleado si se encontro y se elimino exitosamente
                {
                    return Json(new { success = true, deleted = true });
                }
                else
                {
                    return Json(new { success = true, deleted = false });
                }
            }
            
        }

        public JsonResult Actualizar(int id, string cedula, string nombre, string apUno, string apDos, string correo, string tipo, string estado, int puesto, int oficina)
        {
            EmpleadoRN e = new EmpleadoRN();
            int respuesta = e.Actualizar(id, cedula, nombre, apUno, apDos, correo, tipo, estado, puesto, oficina);
            if (respuesta == 1) // El empleado no se encuentra en el sistema
            {
                return Json(new { success = true});
            }
            else
            {
                return Json(new { success = false });
            }

        }



        [HttpPost]
        public JsonResult Obtener(int codigo)
        {
            EmpleadoRN empleadoRN = new EmpleadoRN();
            Empleado empleado= new Empleado();
            empleado.TN_Id_Usuario = codigo;

            return Json(new { resultado = empleadoRN.ObtenerEmpleado(empleado) }); // Retornar el dato solicitado
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

        public string getOficina()
        {
            return Session["UsserOffice"].ToString();
        }
    }
}
