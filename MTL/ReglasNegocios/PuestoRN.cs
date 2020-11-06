using AccesosDatos.Implementaciones;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class PuestoRN
    {


        // Listado de los puestos
        public string ListarPuestos()
        {
            string respuesta = null;
            PuestoAD puestoAD = new PuestoAD();
            //Llamado a accedo de datos
            try
            {
                respuesta = puestoAD.listarPuestos(); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Obtener un puesto  solicitado
        public string ObtenerCircuito(Puesto _puesto)
        {
            PuestoAD puestoAD = new PuestoAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = puestoAD.getPuesto(_puesto); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }






        // Insercion del nuevo puesto al sistema
        public int InsertarPuesto(Puesto _puesto)
        {
            PuestoAD puestoAD = new PuestoAD();
            int respuesta = 0;
            //Llamado a accedo de datos
            try
            {
                respuesta = puestoAD.insertPuesto(_puesto); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }




        // Editar el puesto
        public int EditarPuesto(Puesto _puesto)
        {
            PuestoAD puestoAD = new PuestoAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = puestoAD.updatePuesto(_puesto); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }





        // Eliminar un puesto
        public int EliminarPuesto(Puesto _puesto)
        {

            PuestoAD puestoAD = new PuestoAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = puestoAD.deletePuesto(_puesto); // Resultado de la operacion
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
