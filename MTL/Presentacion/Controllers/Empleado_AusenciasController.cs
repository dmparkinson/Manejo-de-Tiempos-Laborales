﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Empleado_AusenciasController : Controller
    {
        // GET: Empleado_Ausencias
        public ActionResult Listar()
        {

            ViewBag.Message = "Nombre empleado";
            return View();
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
    }
}
