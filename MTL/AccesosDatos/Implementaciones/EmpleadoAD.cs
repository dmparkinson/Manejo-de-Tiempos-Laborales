using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesosDatos.Implementaciones
{
    public class EmpleadoAD : ConexionAD.ConexionAD
    {
        public int registrarEmpleado(Empleado empleado)
        {
            return ejecutar($"exec sp_registrar_empleado sp_registrar_empleado '{empleado.TC_Usuario}', " +
                $"'{empleado.TC_Contrasena}', '{empleado.TC_Identificacion}', '{empleado.TC_Nombre_Usuario}', " +
                    $"'{empleado.TC_Primer_Apellido}', '{empleado.TC_Segundo_Apellido}', '{empleado.TC_Tipo_Usuario}', '{empleado.TC_Correo}', " +
                        $"{empleado.TB_Activo}, {empleado.TN_Id_Puesto}, {empleado.TN_Id_Oficina}, {empleado.TB_Eliminado}");
        }

        public int eliminarEmpleado(int idEmpleado)
        {
            return ejecutar($"exec sp_eliminar_empleado {idEmpleado}");
        }

        public int actualizarEmpleado(Empleado empleado) {
            return ejecutar($"exec sp_actualizar_empleado '{empleado.TC_Usuario}', " +
                $"'{empleado.TC_Contrasena}', '{empleado.TC_Identificacion}', '{empleado.TC_Nombre_Usuario}', " +
                    $"'{empleado.TC_Primer_Apellido}', '{empleado.TC_Segundo_Apellido}', '{empleado.TC_Tipo_Usuario}', '{empleado.TC_Correo}', " +
                        $"{empleado.TB_Activo}, {empleado.TN_Id_Puesto}, {empleado.TN_Id_Oficina}, {empleado.TB_Eliminado}");
        } 

        public string consultarEmpleado(int idEmpleado)
        {
            Empleado emp = new Empleado();
            SqlDataReader dataReader = consultar($"exec sp_consultar_empleado {idEmpleado}");
            while (dataReader.Read()) {
                emp.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                emp.TC_Usuario = dataReader["TC_Usuario"].ToString();
                emp.TC_Contrasena = dataReader["TC_Contrasena"].ToString();
                emp.TC_Identificacion = dataReader["TC_Identificacion"].ToString();
                emp.TC_Nombre_Usuario = dataReader["TC_Nombre_Usuario"].ToString();
                emp.TC_Primer_Apellido = dataReader["TC_Primer_Apellido"].ToString();
                emp.TC_Segundo_Apellido = dataReader["TC_Segundo_Apellido"].ToString();
                emp.TC_Tipo_Usuario = dataReader["TC_Tipo_Usuario"].ToString();
                emp.TC_Correo = dataReader["TC_Correo"].ToString();
                emp.TC_Usuario = dataReader["TC_Usuario"].ToString();
                emp.TB_Activo = int.Parse(dataReader["TB_Activo"].ToString());
                emp.TN_Id_Puesto = int.Parse(dataReader["TN_Id_Puesto"].ToString());
                emp.TN_Id_Oficina = int.Parse(dataReader["TN_Id_Oficina"].ToString());
                emp.TB_Eliminado = int.Parse(dataReader["TB_Eliminado"].ToString());
            }
            
            return JsonConvert.SerializeObject(emp);
        }

        public string listarEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            SqlDataReader dataReader = consultar($"exec sp_listar_empleado");
            while (dataReader.Read())
            {
                Empleado emp = new Empleado();
                emp.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                emp.TC_Usuario = dataReader["TC_Usuario"].ToString();
                emp.TC_Contrasena = dataReader["TC_Contrasena"].ToString();
                emp.TC_Identificacion = dataReader["TC_Identificacion"].ToString();
                emp.TC_Nombre_Usuario = dataReader["TC_Nombre_Usuario"].ToString();
                emp.TC_Primer_Apellido = dataReader["TC_Primer_Apellido"].ToString();
                emp.TC_Segundo_Apellido = dataReader["TC_Segundo_Apellido"].ToString();
                emp.TC_Tipo_Usuario = dataReader["TC_Tipo_Usuario"].ToString();
                emp.TC_Correo = dataReader["TC_Correo"].ToString();
                emp.TC_Usuario = dataReader["TC_Usuario"].ToString();
                emp.TB_Activo = int.Parse(dataReader["TB_Activo"].ToString());
                emp.TN_Id_Puesto = int.Parse(dataReader["TN_Id_Puesto"].ToString());
                emp.TN_Id_Oficina = int.Parse(dataReader["TN_Id_Oficina"].ToString());
                emp.TB_Eliminado = int.Parse(dataReader["TB_Eliminado"].ToString());

                lista.Add(emp);
            }

            return JsonConvert.SerializeObject(lista);
        }
    }
}