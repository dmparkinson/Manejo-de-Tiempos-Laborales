using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Implementaciones
{
    public class RolAD : ConexionAD.ConexionAD
    {


        // Listar todos los roles del sistema
        public string listarRoles()
        {
            List<Rol> lista = new List<Rol>();
            SqlDataReader dataReader = consultar($"exec sp_listar_Roles");
            while (dataReader.Read())
            {
                Rol rol = new Rol();
                rol.TN_Id_Rol = int.Parse(dataReader["TN_Id_Rol"].ToString());
                rol.TC_Nombre_Rol = dataReader["TC_Nombre_Rol"].ToString();
                lista.Add(rol);
            }
            this.closeCon();
            return JsonConvert.SerializeObject(lista);

        }




        // Obtener un rol
        public string getRol(Rol _rol)
        {
            Rol rol = null;
            try
            {
                SqlDataReader dr = consultar("EXEC sp_get_rol '" + _rol.TN_Id_Rol + "'");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        rol = new Rol();
                        rol.TN_Id_Rol = int.Parse(dr["TN_Id_Rol"].ToString());
                        rol.TC_Nombre_Rol = dr["TC_Nombre_Rol"].ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                rol = null;
            }
            this.closeCon();
            return JsonConvert.SerializeObject(rol);
        }





        // Registrar un nuevo rol
        public int insertRol(Rol _rol)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar("EXEC sp_registrar_rol '" + _rol.TC_Nombre_Rol+ "'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                salida = -1;
            }
            this.closeCon();
            return salida;
        }




        // Actualizar los datos de un rol
        public int updateRol(Rol _rol)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC sp_update_rol '" + _rol.TC_Nombre_Rol + "', '" +  _rol.TN_Id_Rol + "'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                salida = -1;
            }
            this.closeCon();
            return salida;
        }




        // Eliminar un rol
        public int deleteRol(Rol _rol)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar($"EXEC sp_delete_rol'{_rol.TN_Id_Rol}'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                salida = -1;
            }
            this.closeCon();
            return salida;
        }
    }
}

