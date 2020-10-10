using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesoDatos.Implementaciones
{
    public class TiposAusencia : ConexionAD.ConexionAD
    {
        public List<string> getTiposAusencia()
        {
            List<string> data = new List<string>();

            try
            {
                SqlDataReader dr = consultar("select * from T_Tipos_Ausencia");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        string json = JsonConvert.SerializeObject(new
                        {
                            tipoAusencia = dr[0]
                        });
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
    }
}