﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error403()
        {
            return View();
        }

        public ActionResult Unknown()
        {
            return View();
        }


        public ActionResult Error404()
        {
            return View();
        }


        public ActionResult Error()
        {
            return View();
        }

   
    }
}