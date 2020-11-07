using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidad
{
    public class Tiempo
    {

        
        public int TN_Id_Tiempo { set; get; }
        public string TF_Fecha { set; get; }
        public string TH_Hora { set; get; }
        public string TC_Tipo { set; get; }
        public int TN_Id_Horario { set; get; }
        public string TC_Horario { set; get; }
        public int TN_Id_Usuario { set; get; }
        public Empleado empleado { set; get; }
        public Horario horario{ set; get; }
    }
}