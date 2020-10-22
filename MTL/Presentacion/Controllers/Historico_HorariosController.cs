using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Historico_HorariosController : Controller
    {
        // GET: Historicos_Horarios
        public ActionResult Listar_de_Admin()
        {

            ViewBag.Message = "Histórico de Horarios";
            return View();
        }
        public ActionResult Listar_de_Jefatura()
        {

            ViewBag.Message = "Histórico de Horarios";
            return View();
        }


    }
}
