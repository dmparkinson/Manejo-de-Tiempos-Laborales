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
    [Autenticado]
    public class Catalogo_CircuitosController : Controller
    {
        // GET: Catalogo_Circuitos

        public ActionResult Listar()
        {
            CircuitoRN circuitoRN = new CircuitoRN();
            List<Circuito> lista = JsonConvert.DeserializeObject<List<Circuito>>(circuitoRN.ListarCircuito());

            ViewBag.ListaCircuitos = lista;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Catálogo de Circuitos";


            if (Session["UsserType"].ToString() == "Administración")
            {
                return View("Listar");
            }
            else
            {
                return View("Error");
            }
        }



        [HttpPost]
        public JsonResult Obtener(int codigo)
        {
            CircuitoRN circuitoRN = new CircuitoRN();
            Circuito circuito = new Circuito();
            circuito.TN_Id_Circuito = codigo;

            return Json(new { resultado = circuitoRN.ObtenerCircuito(circuito) }); // Retornar el dato solicitado
        }




        [HttpPost]
        public JsonResult Insertar(string nombre)
        {
            CircuitoRN circuitoRN = new CircuitoRN();
            Circuito circuito = new Circuito();
            circuito.TC_Desc_Circuito = nombre;

            int respuesta = circuitoRN.InsertarCircuito(circuito);

            if (respuesta == 1) // Los datos se registraron exitosamente
            {
                return Json(new { success = true });
            }
            else  // Error en el registro
            {
                return Json(new { success = false });
            }
        }




        [HttpPost]
        public JsonResult Editar(string nombre, int codigo)
        {
            CircuitoRN circuitoRN = new CircuitoRN();
            Circuito circuito = new Circuito();
            circuito.TC_Desc_Circuito = nombre;
            circuito.TN_Id_Circuito = codigo;

            int respuesta = circuitoRN.EditarCircuito(circuito);

            if (respuesta == 1) // Los datos se modificaron exitosamente 
            {
                return Json(new { success = true });
            }
            else  // Fallo en la edicion de datos
            {
                return Json(new { success = false });
            }
        }





        [HttpPost]
        public JsonResult Eliminar(int codigo)
        {

            CircuitoRN circuitoRN = new CircuitoRN();
            Circuito circuito = new Circuito();
            circuito.TN_Id_Circuito = codigo;

            int respuesta = circuitoRN.EliminarCircuito(circuito);

            if (respuesta == 0) // Los datos a eliminar no se encuentran en el sistema
            {
                return Json(new { success = false, deleted = false });
            }
            else if (respuesta == 1)  // Los datos se encontraron y se elimino exitosamente
            {
                return Json(new { success = true, deleted = true });
            }
            else  // Los datos se encontraron pero no se pudieron eliminar
            {
                return Json(new { success = true, deleted = false });
            }
        }




        [HttpPost]
        public JsonResult Refrescar()
        {
            CircuitoRN circuitoRN = new CircuitoRN();

            return Json(new {resultado = circuitoRN.ListarCircuito() });
        }


    }
}
