using AccesosDatos.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class AusenciaLN
    {


        public string ListarHistoricoAusencias()
        {
            AusenciaAD data = new AusenciaAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = data.getHistoricoAusencias();

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
