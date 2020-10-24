﻿using AccesosDatos.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class TiempoRN
    {
        public TiempoRN() {}
        public void verificarRegistro(string tiempo)
        {
            TiemposAD data = new TiemposAD();

            //primero debemos hacer una lista de los registros que hay
            string lista = data.listarTiempoUsuario();
        }



        public string ListarHistoricoTiempos()
        {
            TiemposAD data = new TiemposAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = data.listarTiempoUsuario();

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
