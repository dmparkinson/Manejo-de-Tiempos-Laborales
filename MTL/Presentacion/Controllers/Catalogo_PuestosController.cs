using Entidad;
using Newtonsoft.Json;
using ReglasNegocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_PuestosController : Controller
    {
        // GET: Catalogo_Puestos
        public ActionResult Listar()
        {
            PuestoRN puestoRN = new PuestoRN();
            List<Puesto> lista = JsonConvert.DeserializeObject<List<Puesto>>(puestoRN.ListarPuestos());

            ViewBag.ListaCatalogoPuestos = lista;
            ViewBag.Message = "Catálogo de Puestos";

            return View();
        }






        [HttpPost]
        public JsonResult Obtener(int codigo)
        {
            PuestoRN puestoRN = new PuestoRN();
            Puesto puesto = new Puesto();
            puesto.TN_Id_Puesto = codigo;

            return Json(new { resultado = puestoRN.ObtenerCircuito(puesto) }); // Retornar el dato solicitado
        }




        [HttpPost]
        public JsonResult Insertar(string nombre, int salario)
        {
            PuestoRN puestoRN = new PuestoRN();
            Puesto puesto = new Puesto();

            puesto.TC_Nombre_Puesto= nombre;
            puesto.TN_Salario_Horario = salario;

            int respuesta = puestoRN.InsertarPuesto(puesto);

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
        public JsonResult Editar(string nombre,int salario, int codigo)
        {
            PuestoRN puestoRN = new PuestoRN();
            Puesto puesto = new Puesto();

            puesto.TC_Nombre_Puesto = nombre;
            puesto.TN_Salario_Horario = salario;
            puesto.TN_Id_Puesto = codigo;

            int respuesta = puestoRN.EditarPuesto(puesto);

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

            PuestoRN puestoRN = new PuestoRN();
            Puesto puesto = new Puesto();
            puesto.TN_Id_Puesto = codigo;

            int respuesta = puestoRN.EliminarPuesto(puesto);

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
            PuestoRN puestoRN = new PuestoRN();

            return Json(new {resultado = puestoRN.ListarPuestos() });
        }
    }
}
