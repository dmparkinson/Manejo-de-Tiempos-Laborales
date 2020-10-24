using AccesosDatos.Implementaciones;
using Entidad;
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
        public string ListarTipoAusencia()
        {
            string respuesta = null;

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
        public int InsertarTipoAusencia(TipoAusencia _tausencia)
        {
            int respuesta = 0;
            string nombre = _tausencia.TC_Tipo_Ausencia;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.insertTiposAusencia(nombre); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }




        // Editar el catalogo/tipo de ausencia
        public int EditarTipoAusencia(string anterior, string nuevo)
        {
            int respuesta = 0;


            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.updateTiposAusencia(anterior, nuevo); // Resultado de la operacion
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
                respuesta = -1;
            }
            return respuesta;
        }

    }
}
