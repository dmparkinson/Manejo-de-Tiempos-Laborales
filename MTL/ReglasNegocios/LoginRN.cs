using AccesosDatos.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocio
{
    public class LoginLN
    {

        public EmpleadoAD empleado = new EmpleadoAD();

        public LoginLN() { }


        public  string LoginUsser(string name, string password) {

            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = empleado.loguearEmpeado(name, password); // Resultado de la operacion
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
