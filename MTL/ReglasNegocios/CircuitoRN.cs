using AccesosDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class CircuitoRN
    {
        // Listado de los tipos de circuitos
        public string ListarCircuito()
        {
            string respuesta = null;
            CircuitoAD circuitoAD = new CircuitoAD();
            //Llamado a accedo de datos
            try
            {
                respuesta = circuitoAD.listarCircuito(); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Obtener un circuito  solicitado
        public string ObtenerCircuito(Circuito _circuito)
        {
            CircuitoAD circuitoAD = new CircuitoAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = circuitoAD.getCircuito(_circuito); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }






        // Insercion del nuevo circuito al sistema
        public int InsertarCircuito(Circuito _circuito)
        {
            CircuitoAD circuitoAD = new CircuitoAD();
            int respuesta = 0;
            //Llamado a accedo de datos
            try
            {
                respuesta = circuitoAD.insertCircuito(_circuito); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }




        // Editar el circuito
        public int EditarCircuito(Circuito _circuito)
        {
            CircuitoAD circuitoAD = new CircuitoAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = circuitoAD.updateCircuito(_circuito); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Eliminar un circuito
        public int EliminarCircuito(Circuito _circuito)
        {

            CircuitoAD circuitoAD = new CircuitoAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = circuitoAD.deleteCircuito(_circuito); // Resultado de la operacion
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
