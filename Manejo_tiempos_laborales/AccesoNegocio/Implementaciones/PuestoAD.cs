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
    public class PuestoAD : ConexionAD.ConexionAD
    {


        // Listar todos los puestos del sistema
        public string listarPuestos()
        {
            List<Puesto> lista = new List<Puesto>();
            SqlDataReader dataReader = consultar($"exec sp_listar_puesto");
            while (dataReader.Read())
            {
                Puesto puesto = new Puesto();
                puesto.TN_Id_Puesto = int.Parse(dataReader["TN_Id_Puesto"].ToString());
                puesto.TC_Nombre_Puesto = dataReader["TC_Nombre_Puesto"].ToString();
                puesto.TN_Salario_Horario = int.Parse(dataReader["TN_Salario_Horario"].ToString());
                
                lista.Add(puesto);
            }
            return JsonConvert.SerializeObject(lista);

        }




        // Obtener un puesto
        public string getPuesto(Puesto _puesto)
        {
            Puesto puesto = null;
            try
            {
                SqlDataReader dr = consultar("EXEC sp_get_puesto '" + _puesto.TN_Id_Puesto+ "'");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        puesto = new Puesto();
                        puesto.TN_Id_Puesto = int.Parse(dr["TN_Id_Puesto"].ToString());
                        puesto.TC_Nombre_Puesto = dr["TC_Nombre_Puesto"].ToString();
                        puesto.TN_Salario_Horario = int.Parse(dr["TN_Salario_Horario"].ToString());
                    }
                }
            }
            catch (SqlException e)
            {
                puesto = null;
            }
            return JsonConvert.SerializeObject(puesto);
        }





        // Registrar un nuevo puesto
        public int insertPuesto(Puesto _puesto)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar("EXEC insert_puesto '" + _puesto.TC_Nombre_Puesto + "','"+_puesto.TN_Salario_Horario +"'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }
            return salida;
        }




        // Actualizar los datos de un puesto
        public int updatePuesto(Puesto _puesto)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC sp_update_puesto '" + _puesto.TC_Nombre_Puesto+ "', '" + _puesto.TN_Id_Puesto+ "', '" +_puesto.TN_Salario_Horario+ "'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }

            return salida;
        }




        // Eliminar un puesto
        public int deletePuesto(Puesto _puesto)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar($"EXEC delete_puesto'{_puesto.TN_Id_Puesto}'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }
            return salida;
        }
    }
}
