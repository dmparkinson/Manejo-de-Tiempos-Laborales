using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Empleado
    {
		public int TN_Id_Usuario { set; get; }
		public string TC_Usuario { set; get; }
		public string TC_Contrasena { set; get; }
		public string TC_Identificacion { set; get; }
		public string TC_Nombre_Usuario { set; get; }
		public string TC_Primer_Apellido { set; get; }
		public string TC_Segundo_Apellido { set; get; }
		public string TC_Tipo_Usuario { set; get; }
		public string TC_Correo { set; get; }
		public int TB_Activo { set; get; }
		public int TN_Id_Puesto { set; get; }
		public string TC_Nombre_Puesto { set; get; }
		public int TN_Id_Oficina { set; get; }
		public string TC_Nombre_Oficina { set; get; }
		public int TB_Eliminado { set; get; }
	}
}
