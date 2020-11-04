using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Oficina
    {
        public int TN_Id_Oficina { set; get; }
        public string TC_Nombre_Oficina { set; get; }
        public string TC_Codigo { set; get; }
        public string TC_Desc_Circuito { set; get; }
        public int TB_Activa { set; get; }
        public string TF_Inicio_Vigencia { set; get; }
        public string TF_Fin_Vigencia { set; get; }
    }
}
