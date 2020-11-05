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
            int salida = -1;
            SqlDataReader dr = consultar($"exec sp_registrar_empleado '{empleado.TC_Usuario}', " +
                $"'{empleado.TC_Contrasena}', '{empleado.TC_Identificacion}', '{empleado.TC_Nombre_Usuario}', " +
                    $"'{empleado.TC_Primer_Apellido}', '{empleado.TC_Segundo_Apellido}', '{empleado.TC_Tipo_Usuario}', '{empleado.TC_Correo}', " +
                        $"{empleado.TB_Activo}, {empleado.TN_Id_Puesto}, {empleado.TN_Id_Oficina}");
            dr.Read();
            salida = int.Parse(dr[0].ToString());
            closeCon();
            return salida;
        }

        public int eliminarEmpleado(int id)
        {
            int salida = -1;
            SqlDataReader dr = consultar($"exec sp_eliminar_empleado '{id}'");
            dr.Read();
            salida = int.Parse(dr[0].ToString());
            closeCon();
            return salida;
        }

        public int actualizarEmpleado(Empleado empleado) {
            int salida = -1;
            SqlDataReader dr = consultar($"exec sp_actualizar_empleado {empleado.TN_Id_Usuario},'{empleado.TC_Identificacion}','{empleado.TC_Nombre_Usuario}','{empleado.TC_Primer_Apellido}'" +
                $",'{empleado.TC_Segundo_Apellido}','{empleado.TC_Tipo_Usuario}','{empleado.TC_Correo}',{empleado.TB_Activo},{empleado.TN_Id_Puesto},{empleado.TN_Id_Oficina}");
            dr.Read();
            salida = int.Parse(dr[0].ToString());
            closeCon();
            return salida;
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



        public string loguearEmpeado(string _usuario, string _contrasenia)
        {
            Empleado empleado = new Empleado();
            SqlDataReader dataReader = consultar($"exec sp_login_usuario '{_usuario}','{_contrasenia}' ");
            while (dataReader.Read())
            {
                empleado.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                empleado.TC_Usuario = dataReader["TC_Usuario"].ToString();
                empleado.TC_Contrasena = dataReader["TC_Contrasena"].ToString();
                empleado.TC_Identificacion = dataReader["TC_Identificacion"].ToString();
                empleado.TC_Nombre_Usuario = dataReader["TC_Nombre_Usuario"].ToString();
                empleado.TC_Primer_Apellido = dataReader["TC_Primer_Apellido"].ToString();
                empleado.TC_Segundo_Apellido = dataReader["TC_Segundo_Apellido"].ToString();
                empleado.TC_Tipo_Usuario = dataReader["TC_Tipo_Usuario"].ToString();
                empleado.TC_Correo = dataReader["TC_Correo"].ToString();
                empleado.TC_Usuario = dataReader["TC_Usuario"].ToString();
                empleado.TB_Activo = int.Parse(dataReader["TB_Activo"].ToString());
                empleado.TN_Id_Puesto = int.Parse(dataReader["TN_Id_Puesto"].ToString());
                empleado.TN_Id_Oficina = int.Parse(dataReader["TN_Id_Oficina"].ToString());
                empleado.TB_Eliminado = int.Parse(dataReader["TB_Eliminado"].ToString());
            }

            this.closeCon();
            return JsonConvert.SerializeObject(empleado);
        }



        public string listarEmpleados()
        {
            List<Empleado> lista = new List<Empleado>();
            SqlDataReader dataReader = consultar($"EXEC sp_listar_empleados");
            while (dataReader.Read())
            {

                Empleado emp = new Empleado();
                emp.TN_Id_Usuario = int.Parse(dataReader[0].ToString());
                emp.TC_Usuario = dataReader[1].ToString();
                emp.TC_Contrasena = dataReader[2].ToString();
                emp.TC_Identificacion = dataReader[3].ToString();
                emp.TC_Nombre_Usuario = dataReader[4].ToString();
                emp.TC_Primer_Apellido = dataReader[5].ToString();
                emp.TC_Segundo_Apellido = dataReader[6].ToString();
                emp.TC_Tipo_Usuario = dataReader[7].ToString();
                emp.TC_Correo = dataReader[8].ToString();
                emp.TB_Activo = int.Parse(dataReader[9].ToString());
                emp.TC_Nombre_Puesto = dataReader[11].ToString();
                emp.TC_Nombre_Oficina = dataReader[10].ToString();
                emp.TB_Eliminado = int.Parse(dataReader[12].ToString());

                lista.Add(emp);
            }
            closeCon();
            return JsonConvert.SerializeObject(lista);
        }

        public string listarOficinasEmpleado()
        {
            List<Oficina> lista = new List<Oficina>();   
            SqlDataReader dataReader = consultar($"EXEC sp_listar_oficinas");
            while (dataReader.Read())
            {

                Oficina o = new Oficina();
                o.TN_Id_Oficina = int.Parse(dataReader[0].ToString());
                o.TC_Nombre_Oficina = dataReader[1].ToString();

                lista.Add(o);
            }
            closeCon();
            return JsonConvert.SerializeObject(lista);
        }

        public string listarPuestosEmpleado()
        {
            List<Puesto> lista = new List<Puesto>();
            SqlDataReader dataReader = consultar($"EXEC sp_listar_puesto");
            while (dataReader.Read())
            {

                Puesto p = new Puesto();
                p.TN_Id_Puesto = int.Parse(dataReader[0].ToString());
                p.TC_Nombre_Puesto = dataReader[1].ToString();

                lista.Add(p);
            }
            closeCon();
            return JsonConvert.SerializeObject(lista);
        }
    }
}