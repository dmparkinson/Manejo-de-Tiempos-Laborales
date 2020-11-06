using AccesosDatos.Implementaciones;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class RolRN
    {


        // Listado de los tipos de roles
        public string ListarRoles()
        {
            string respuesta = null;
            RolAD rolAD = new RolAD();
            //Llamado a accedo de datos
            try
            {
                respuesta = rolAD.listarRoles(); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Obtener un rol  solicitado
        public string ObtenerRol(Rol _rol)
        {
            RolAD rolAD = new RolAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = rolAD.getRol(_rol); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }






        // Insercion del nuevo rol al sistema
        public int InsertarRol(Rol _rol)
        {
            RolAD rolAD = new RolAD();
            int respuesta = 0;
            //Llamado a accedo de datos
            try
            {
                respuesta = rolAD.insertRol(_rol); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }




        // Editar el rol
        public int EditarRol(Rol _rol)
        {
            RolAD rolAD = new RolAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = rolAD.updateRol(_rol); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Eliminar un rol
        public int EliminarRol(Rol _rol)
        {

            RolAD rolAD = new RolAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = rolAD.deleteRol(_rol); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = -1;
            }
            return respuesta;
        }
    }
}
