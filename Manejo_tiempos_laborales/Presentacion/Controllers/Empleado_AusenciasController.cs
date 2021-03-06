﻿using Entidad;
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
    public class Empleado_AusenciasController : Controller
    {
        // GET: Empleado_Ausencias
        public ActionResult Listar()
        {
            AusenciaRN a = new AusenciaRN();
            TipoAusenciaRN tAusencia = new TipoAusenciaRN();
            ViewBag.Lista = a.ListarAusenciasEmpleado(int.Parse(Session["EmpAus"].ToString()));
            List<TipoAusencia> tipos = JsonConvert.DeserializeObject<List<TipoAusencia>>(tAusencia.ListarTiposAusencia());

            ViewBag.Tipos = tipos;
            ViewBag.Nombre = Session["NombreCompleto"].ToString();

            ViewBag.Tipos = tipos;

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

        public JsonResult EmpleadoAusencia(int idEmpleado, string nombre, string apUno, string apDos)
        {
            Session["EmpAus"] = idEmpleado;
            Session["NombreCompleto"] = nombre + " " + apUno + " " + apDos;
            return Json(new {url = Url.Action("Listar", "Empleado_Ausencias") });
        }

        public JsonResult EmpleadoAusenciaJ(int idEmpleado, string nombre, string apUno, string apDos)
        {
            Session["EmpAus"] = idEmpleado;
            Session["NombreCompleto"] = nombre + " " + apUno + " " + apDos;
            return Json(new { url = Url.Action("Listar", "Empleado_Ausencias") });
        }



        public ActionResult Listar_de_Empleado()
        {

            AusenciaRN a = new AusenciaRN();
            TipoAusenciaRN tAusencia = new TipoAusenciaRN();
            ViewBag.Lista = a.ListarAusenciasEmpleado(int.Parse(Session["UsserID"].ToString()));
            List<TipoAusencia> tipos = JsonConvert.DeserializeObject<List<TipoAusencia>>(tAusencia.ListarTiposAusencia());
            ViewBag.Tipos = tipos;
            ViewBag.Message = Session["UsserName"].ToString() + " " + Session["UsserSurname1"].ToString() + " " + Session["UsserSurname2"].ToString();

            if ( (Session["UsserType"].ToString() == "Administración") ||
                (Session["UsserType"].ToString() == "Jefatura") ||
                (Session["UsserType"].ToString() == "Estándar") )
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error403", "Error");
            }

        }

        public JsonResult InsertarAusencia(string fechai, string fechaf, int motivo)
        {
            AusenciaRN a = new AusenciaRN();
            int respuesta = a.InsertarAusencia(fechai, fechaf, motivo, int.Parse(Session["EmpAus"].ToString()));
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

        public JsonResult EliminarAusencia(string id)
        {
            AusenciaRN a = new AusenciaRN();
            int respuesta = a.EliminarAusencia(id);
            if (respuesta == 1) // El tipo de ausencia se agregoexitosamente 
            {
                return Json(new { success = true, deleted = true });
            }
            else  // El tipo de ausencia no se registro en el sistema
            {
                if (respuesta == 0)
                {
                    return Json(new { success = true, deleted = false });
                }
                else
                {
                    return Json(new { success = false, deleted = false });
                }
            }
        }

        public JsonResult EditarAusencia(string idAusencia, string tipo, string fechai, string fechaf)
        {
            AusenciaRN a = new AusenciaRN();
            int respuesta = a.EditarAusencia(idAusencia, Session["EmpAus"].ToString(), tipo,fechai,fechaf);
            if (respuesta == 1) // El tipo de ausencia se agregoexitosamente 
            {
                return Json(new { success = true, updated = true });
            }
            else  // El tipo de ausencia no se registro en el sistema
            {
                if (respuesta == 0)
                {
                    return Json(new { success = true, updated = false });
                }
                else
                {
                    return Json(new { success = false, updated = false });
                }
            }
        }

        public string refrescarAusencias()
        {
            AusenciaRN a = new AusenciaRN();
            return JsonConvert.SerializeObject(a.ListarAusenciasEmpleado(int.Parse(Session["EmpAus"].ToString())));
        }

        // GET: Empleado_Ausencias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Empleado_Ausencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleado_Ausencias/Create
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

        // GET: Empleado_Ausencias/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Empleado_Ausencias/Edit/5
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

        // GET: Empleado_Ausencias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Empleado_Ausencias/Delete/5
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

        public string GetAusencia(int id)
        {
            AusenciaRN a = new AusenciaRN();

            return JsonConvert.SerializeObject(a.getAusencia(id));
        }
    }
}
