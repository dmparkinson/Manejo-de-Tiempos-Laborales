using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesoDatos.Implementaciones
{
    public class TiposAusenciaAD : ConexionAD.ConexionAD
    {
        public List<string> getTiposAusencia(string busqueda)
        {
            List<string> data = new List<string>();

            try
            {
                SqlDataReader dr = consultar("EXEC get_tipos_ausencia '%"+busqueda+"%'");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        string json = JsonConvert.SerializeObject(new
                        {
                            tipoAusencia = dr[0].ToString()
                        }) ;
                        data.Add(json);
                    }
                }
                else
                {
                    data.Add("Error en la conexion");
                }
            }
            catch (SqlException e)
            {
                data.Add("Error en la conexion");
            }
            return data;
        }

        public int insertTiposAusencia(string nombre)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC insert_tipo_ausencia '"+nombre+"'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }

            return salida;
        }

        public int updateTiposAusencia(string nombre, string nuevo)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC update_tipo_ausencia '"+nombre+"', '"+nuevo+"'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }

            return salida;
        }

        public int deleteTipoAusencia(string nombre)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC delete_tipo_ausencia '"+nombre+"'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = 0;
            }
            return salida;
        }
    }
}