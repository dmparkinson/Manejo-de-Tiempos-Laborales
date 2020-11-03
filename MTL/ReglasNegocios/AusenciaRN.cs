using AccesosDatos.Implementaciones;
using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class AusenciaRN
    {

        public AusenciaAD ausenciaAD = new AusenciaAD();


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






        // Editar el ausencia
        public int EditarAusencia(string idAusencia, string idUsuario, string tipoNuevo, string fechaSalidaNuevo, string fechaRegresoNuevo)
        {
            int respuesta = 0;

            int id = int.Parse(idAusencia);
            int usuario = int.Parse(idUsuario);
            string fechaS = fechaSalidaNuevo + " 00:00:00.000";
            string fechaR = fechaRegresoNuevo + " 00:00:00.000";

            Ausencia ausenciaNueva = new Ausencia(id, fechaS, fechaR, tipoNuevo, usuario);

            //Llamado a accedo de datos
            try
            {
                respuesta = ausenciaAD.updateAusencia(ausenciaNueva); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Eliminar el ausencia
        public int EliminarAusencia(string _id)
        {
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = ausenciaAD.deleteAusencia(int.Parse(_id)); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = -1;
            }
            return respuesta;
        }

        public List<Ausencia> ListarAusenciasEmpleado(int idEmpleado)
        {
            AusenciaAD a = new AusenciaAD();
            string respuesta = null;
            List<Ausencia> lista = new List<Ausencia>();
            try
            {
                respuesta = a.getAllAusencias(idEmpleado);
                if (respuesta != null)
                {
                    lista = JsonConvert.DeserializeObject<List<Ausencia>>(respuesta);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return lista;
        }
    }
}
