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
    public class Catalogo_RolesController : Controller
    {

        // GET: Catalogo_Rol
        [Authorize]
        [Autenticado]
        public ActionResult Listar()
        {
            RolRN rolRN = new RolRN();
            List<Rol> lista = JsonConvert.DeserializeObject<List<Rol>>(rolRN.ListarRoles());

            ViewBag.ListaCatalogoRoles = lista;
            ViewBag.Message = "Catálogo de Roles";

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
            RolRN rolRN = new RolRN();
            Rol rol = new Rol();
            rol.TN_Id_Rol = codigo;

            return Json(new { resultado = rolRN.ObtenerRol(rol) }); // Retornar el dato solicitado
        }







        [HttpPost]
        public JsonResult Insertar(string nombre)
        {
            RolRN rolRN = new RolRN();
            Rol rol = new Rol();
            rol.TC_Nombre_Rol = nombre;

            int respuesta = rolRN.InsertarRol(rol);

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
            RolRN rolRN = new RolRN();
            Rol rol = new Rol();
            rol.TC_Nombre_Rol= nombre;
            rol.TN_Id_Rol= codigo;

            int respuesta = rolRN.EditarRol(rol);

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

            RolRN rolRN = new RolRN();
            Rol rol = new Rol();
            rol.TN_Id_Rol = codigo;

            int respuesta = rolRN.EliminarRol(rol);

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
            RolRN rolRN = new RolRN();

            return Json(new { resultado = rolRN.ListarRoles() });
        }
    }
}
