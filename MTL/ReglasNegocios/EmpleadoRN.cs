using AccesosDatos.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class EmpleadoRN
    {


        public string ListarEmpleados()
        {
            EmpleadoAD data = new EmpleadoAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = data.listarEmpleados(); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }
    }
}
