using AccesosDatos.Implementaciones;
using Entidades;
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
        public int EditarAusencia(string _id, string _fSalida, string _fRegreso,string _idUsuario,  string _idTipoAusencia)
        {
            int respuesta = 0;
            Ausencia ausenciaNueva = new Ausencia(int.Parse(_id), _fSalida, _fRegreso, _idTipoAusencia, int.Parse(_idUsuario) );

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
    }
}
