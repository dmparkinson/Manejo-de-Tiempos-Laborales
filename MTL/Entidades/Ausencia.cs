using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Ausencia
    {
        
        public int TN_Id_Ausencia { set; get; }
        public string TF_Fecha_Salida { set; get; }
        public string TF_Fecha_Regreso { set; get; }
        public string TC_Tipo_Ausencia { set; get; }
        public int TN_Id_Usuario { set; get; }
        public Empleado empleado { set; get; }
    }
}
