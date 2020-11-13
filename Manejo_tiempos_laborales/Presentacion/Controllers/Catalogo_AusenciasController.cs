using Entidad;
using Newtonsoft.Json;
using Presentacion.Security;
using ReglasNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    [Autenticado]
    public class Catalogo_AusenciasController : Controller
    {
        // GET: Catalogo_Ausencias
        
        public ActionResult Listar()
        {
            TipoAusenciaRN tAusenciaNR = new TipoAusenciaRN();
            List<TipoAusencia> lista = JsonConvert.DeserializeObject<List<TipoAusencia>>(tAusenciaNR.ListarTiposAusencia());

            ViewBag.ListaCatalogoAusencias = lista;
            ViewBag.Respuesta = "";
            ViewBag.Message = "Catalogo Ausencias";

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
            TipoAusenciaRN tAusenciaNR = new TipoAusenciaRN();
            TipoAusencia buscar = new TipoAusencia();
            buscar.TN_Id_Tipo_Ausencia = codigo; 

            return Json(new { resultado = tAusenciaNR.ObtenerTipoAusencia(buscar) }); // Retornar el dato solicitado
        }





        [HttpPost]
        public JsonResult  Insertar(string nombre)
        {
            TipoAusenciaRN tAusenciaNR = new TipoAusenciaRN();
            TipoAusencia tAusencia = new TipoAusencia();
            tAusencia.TC_Tipo_Ausencia = nombre;

            int respuesta = tAusenciaNR.InsertarTipoAusencia(tAusencia);

            if (respuesta == 1) // El tipo de ausencia se agregoexitosamente 
            {
                return Json(new { success = true });
            }
            else  // El tipo de ausencia no se registro en el sistema
            {
                return Json(new { success = false });
            }
        }




        [HttpPost]
        public JsonResult  Editar(string nombre, int codigo)
        {
            TipoAusenciaRN tAusenciaNR = new TipoAusenciaRN();
            TipoAusencia tAusencia = new TipoAusencia();


            tAusencia.TC_Tipo_Ausencia = nombre;
            tAusencia.TN_Id_Tipo_Ausencia = codigo;

            int respuesta = tAusenciaNR.EditarTipoAusencia(tAusencia);

            if (respuesta == 1) // El tipo de ausencia se modifico exitosamente 
            {
                return Json(new { success = true });
            }
            else  // El tipo de ausencia no se modifico 
            {
                return Json(new { success = false });
            }
        }





        [HttpPost]
        public JsonResult  Eliminar(int codigo)
        {

            TipoAusenciaRN tAusenciaNR = new TipoAusenciaRN();
            TipoAusencia tAusencia = new TipoAusencia();
            tAusencia.TN_Id_Tipo_Ausencia = codigo;
            int respuesta = tAusenciaNR.EliminarTipoAusencia(tAusencia);

            if (respuesta == 0) // El tipo de ausencia no se encuentra en el sistema
            {
                return Json(new { success = false, deleted = false });
            }
            else if(respuesta == 1)  // El tipo de ausencia si se encontro y se elimino exitosamente
            {
                return Json(new { success = true, deleted = true});
            }
            else  // El tipo de ausencia si se encontro pero se encuentra relacionado asi que no se elimino
            {
                return Json(new { success = true, deleted = false  });
            }
        }





        [HttpPost]
        public JsonResult Refrescar()
        {
            TipoAusenciaRN tAusenciaNR = new TipoAusenciaRN();

            return Json(new {resultado = tAusenciaNR.ListarTiposAusencia() });          
        }

        



    }
        
}
