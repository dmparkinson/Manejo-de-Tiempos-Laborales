using AccesosDatos.Implementaciones;
using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocio
{
    public class TipoAusenciaRN
    {

        // Listado de los tipos de ausencias
        public string ListarTiposAusencia()
        {
            string respuesta = null;
            TiposAusenciaAD tAusenciaAD = new TiposAusenciaAD();
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



        // Obtener un tipo de ausencia  solicitado
        public string ObtenerTipoAusencia(TipoAusencia _tAusencia)
        {
            TiposAusenciaAD tAusenciaAD = new TiposAusenciaAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.getTipoAusencia(_tAusencia); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }






        // Insercion del nuevo tipo ausencia al sistema
        public int InsertarTipoAusencia(TipoAusencia _tAusencia)
        {
            TiposAusenciaAD tAusenciaAD = new TiposAusenciaAD();
            int respuesta = 0;
            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.insertTiposAusencia(_tAusencia); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }




        // Editar un tipo de ausencia
        public int EditarTipoAusencia(TipoAusencia _tAusencia)
        {
            TiposAusenciaAD tAusenciaAD = new TiposAusenciaAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.updateTiposAusencia(_tAusencia); // Resultado de la operacion
                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }



        // Eliminar un tipo de ausencia
        public int EliminarTipoAusencia(TipoAusencia _tAusencia)
        {

            TiposAusenciaAD tAusenciaAD = new TiposAusenciaAD();
            int respuesta = 0;

            //Llamado a accedo de datos
            try
            {
                respuesta = tAusenciaAD.deleteTipoAusencia(_tAusencia); // Resultado de la operacion
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
