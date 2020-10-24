using Entidad;
using Newtonsoft.Json;
using ReglasNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class Catalogo_AusenciasController : Controller
    {
        // GET: Catalogo_Ausencias
        public ActionResult Listar()
        {
            TipoAusenciaLN tAusencia = new TipoAusenciaLN();


            List<TipoAusencia> lista = JsonConvert.DeserializeObject<List<TipoAusencia>>(tAusencia.ListarTipoAusencia());

            ViewBag.ListaCatalogoAusencias = lista;
            ViewBag.Respuesta = "";

            return View();
        }



        [HttpPost]
        public JsonResult  Insertar(string nombre)
        {
            TipoAusenciaLN tAusenciaNR = new TipoAusenciaLN();

            int respuesta = tAusenciaNR.InsertarTipoAusencia(nombre);

            if (respuesta == 1) // El tipo de ausencia se agregoexitosamente 
            {
                return Json(new { success = true, dato = nombre });
            }
            else  // El tipo de ausencia no se registro en el sistema
            {
                return Json(new { success = false });
            }
        }




        [HttpPost]
        public JsonResult  Editar(string anterior, string nuevo)
        {
            TipoAusenciaLN tAusencia = new TipoAusenciaLN();

            int respuesta = tAusencia.EditarTipoAusencia(anterior, nuevo);

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
        public JsonResult  Eliminar(string _tipoAusencia)
        {

            TipoAusenciaLN tAusencia = new TipoAusenciaLN();
            int respuesta = tAusencia.EliminarTipoAusencia(_tipoAusencia);

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

        





    }
        
}
