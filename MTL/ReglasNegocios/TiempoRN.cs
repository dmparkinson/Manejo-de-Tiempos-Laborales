using AccesosDatos.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    class TiempoRN
    {
        public void verificarRegistro(string tiempo)
        {
            TiemposAD data = new TiemposAD();

            //primero debemos hacer una lista de los registros que hay
            string lista = data.listarTiempoUsuario();
        }
    }
}
