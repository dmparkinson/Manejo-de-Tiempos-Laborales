using Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Ausencia
    {
        public Ausencia()
        {
        }

        public Ausencia(int tN_Id_Ausencia, string tF_Fecha_Salida, string tF_Fecha_Regreso, string tC_Tipo_Ausencia, int tN_Id_Usuario)
        {
            TN_Id_Ausencia = tN_Id_Ausencia;
            TF_Fecha_Salida = tF_Fecha_Salida;
            TF_Fecha_Regreso = tF_Fecha_Regreso;
            TC_Tipo_Ausencia = tC_Tipo_Ausencia;
            TN_Id_Usuario = tN_Id_Usuario;
        }


        public int TN_Id_Ausencia { set; get; }
        public string TF_Fecha_Salida { set; get; }
        public string TF_Fecha_Regreso { set; get; }
        public string TC_Tipo_Ausencia { set; get; }
        public int TN_Id_Usuario { set; get; }
        public Empleado empleado { set; get; }
    }
}
