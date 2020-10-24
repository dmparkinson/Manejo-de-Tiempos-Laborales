using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class TipoAusencia
    {
        public TipoAusencia(string _tC_Tipo_Ausencia)
        {
            TC_Tipo_Ausencia = _tC_Tipo_Ausencia;
        }

        public TipoAusencia()
        {
        }

        public string TC_Tipo_Ausencia { set; get; }
    }
}
