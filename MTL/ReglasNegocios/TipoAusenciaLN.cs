using AccesosDatos.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocio
{
    public class TipoAusenciaLN
    {

        public TiposAusenciaAD tAusenciaAD = new TiposAusenciaAD();

        public TipoAusenciaLN(){}




        // Listado de las categorais/tipos de ausencias disponibles
        public List<string> ListarTipoAusencia()
        {
            List<string> respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.listarTiposAusencia(); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Insercion de un nuevo tipo/categoria de ausencia de usuarios
        public int InsertarTipoAusencia(string _nombre)
        {
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.insertTiposAusencia(_nombre); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }




        // Editar el catalogo/tipo de ausencia
        public int EditarTipoAusencia(string _nombreAnterior, string _nuevoNombre)
        {
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.updateTiposAusencia(_nombreAnterior, _nuevoNombre); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Eliminar el catalogo/tipo de ausencia
        public int EliminarTipoAusencia(string _nombre)
        {
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.deleteTipoAusencia(_nombre); // Resultado de la operacion
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
