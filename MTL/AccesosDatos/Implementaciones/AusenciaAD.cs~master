using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesosDatos.Implementaciones
{
    public class AusenciaAD : ConexionAD.ConexionAD
    {
        public List<string> getAllAusencias(int idUsuario)
        {
            List<string> data = new List<string>();

            try
            {
                SqlDataReader dr = consultar("EXEC get_all_ausencias "+idUsuario+"");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        string json = JsonConvert.SerializeObject(new
                        {
                            idAusencia = int.Parse(dr[0].ToString()),
                            fechaSalida = dr[1].ToString(),
                            fechaRegreso = dr[2].ToString(),
                            tipoAusencia = dr[3].ToString()
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

        public List<string> getAusencias(string fechaS, string fechaR, string tipoA,int idUsuario)
        {
            List<string> data = new List<string>();

            try
            {
                SqlDataReader dr = consultar("EXEC get_ausencias '"+fechaS+"','"+fechaR+"','"+tipoA+"',"+idUsuario+"");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        string json = JsonConvert.SerializeObject(new
                        {
                            idAusencia = int.Parse(dr[0].ToString()),
                            fechaSalida = dr[1].ToString(),
                            fechaRegreso = dr[2].ToString(),
                            tipoAusencia = dr[3].ToString()
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

        public int insertAusencia(string fechaS, string fechaR, string tipoA, int idUsuario)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC insert_ausencias '" + fechaS + "','" + fechaR + "','" + tipoA + "'," + idUsuario + "");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }

            return salida;
        }

        public int updateAusencia(string fechaS, string fechaR, string tipoA, int idUsuario, int idAusencia)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC update_ausencias '"+fechaS+"','"+fechaR+"','"+tipoA+"',"+idUsuario+","+idAusencia+"");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }

            return salida;
        }

        public int deleteAusencia(int idAusencia)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC delete_ausencias "+idAusencia+"");
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