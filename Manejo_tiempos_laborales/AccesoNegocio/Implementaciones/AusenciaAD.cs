using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Windows;

namespace AccesoDatos.Implementaciones
{
    public class AusenciaAD : ConexionAD.ConexionAD
    {
        public string getAllAusencias(int idUsuario)
        {
            List<Ausencia> data = new List<Ausencia>();
            try
            {
                SqlDataReader dr = consultar($"EXEC get_all_ausencias {idUsuario}");
                
                    while (dr.Read())
                    {
                        Ausencia a = new Ausencia();
                        a.TN_Id_Ausencia = int.Parse(dr[0].ToString());
                        int aux = dr[1].ToString().IndexOf(" ");
                        String aux2 = dr[1].ToString().Substring(0, aux);
                        a.TF_Fecha_Salida = DateTime.Parse(aux2).ToString("dd/MM/yyyy");
                        aux = dr[2].ToString().IndexOf(" ");
                        aux2 = dr[2].ToString().Substring(0, aux);
                        a.TF_Fecha_Regreso = DateTime.Parse(aux2).ToString("dd/MM/yyyy");
                        a.TC_Tipo_Ausencia = dr[3].ToString();
                        
                        data.Add(a);
                    }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return JsonConvert.SerializeObject(data);
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
                Console.WriteLine(e.ToString());
                data.Add("Error en la conexion");
            }
            this.closeCon();
            return data;
        }

        public int insertAusencia(Ausencia _ausencia)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar($"EXEC insert_ausencias '{_ausencia.TF_Fecha_Salida}','{_ausencia.TF_Fecha_Regreso}','{_ausencia.TN_Id_Tipo_Ausencia}',{_ausencia.TN_Id_Usuario}");
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




        public int updateAusencia(Ausencia ausencia)
        {
            int salida = 0;

            try
            {

                SqlDataReader dr = consultar($"EXEC update_ausencias '{ ausencia.TF_Fecha_Salida}','{ausencia.TF_Fecha_Regreso}',{ausencia.TN_Id_Tipo_Ausencia},{ausencia.TN_Id_Usuario},{ausencia.TN_Id_Ausencia}");
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

        public int deleteAusencia(int idAusencia)
        {
            int salida = -1;

            try
            {
                SqlDataReader dr = consultar($"EXEC delete_ausencias {idAusencia}");
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




        public string getHistoricoAusencias()
        {
            List<Ausencia> lista = new List<Ausencia>();
            try
            {
                SqlDataReader dataReader = consultar($"exec sp_historico_ausencias");

                while (dataReader.Read())
                {
                    Ausencia ausencia = new Ausencia();

                    string[] fecha_salida = dataReader["TF_Fecha_Salida"].ToString().Split(' ');
                    string[] fecha_regreso = dataReader["TF_Fecha_Regreso"].ToString().Split(' ');

                    ausencia.TN_Id_Ausencia = int.Parse(dataReader["TN_Id_Ausencia"].ToString());
                    ausencia.TF_Fecha_Salida = fecha_salida[0];
                    ausencia.TF_Fecha_Regreso = fecha_regreso[0];
                    ausencia.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());

                    ausencia.empleado = new Empleado();
                    ausencia.empleado.TC_Identificacion = dataReader["TC_Identificacion"].ToString();
                    ausencia.empleado.TC_Nombre_Usuario = dataReader["TC_Nombre_Usuario"].ToString();
                    ausencia.empleado.TC_Primer_Apellido = dataReader["TC_Primer_Apellido"].ToString();
                    ausencia.empleado.TC_Segundo_Apellido = dataReader["TC_Segundo_Apellido"].ToString();

                    ausencia.tipoAusencia = new TipoAusencia();
                    ausencia.tipoAusencia.TN_Id_Tipo_Ausencia = int.Parse(dataReader["TN_Id_Tipo_Ausencia"].ToString());
                    ausencia.tipoAusencia.TC_Tipo_Ausencia = dataReader["TC_Tipo_Ausencia"].ToString();

                    lista.Add(ausencia);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return JsonConvert.SerializeObject(lista);

        }

        

        // Obtener una ausencia
        public string getAusencia(Ausencia _ausencia)
        {
            Ausencia ausencia= null;
            try
            {
                SqlDataReader dataReader = consultar("EXEC get_ausencia '" + _ausencia.TN_Id_Ausencia + "'");
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        string[] fecha_salida = dataReader["TF_Fecha_Salida"].ToString().Split(' ');
                        string[] fecha_regreso = dataReader["TF_Fecha_Regreso"].ToString().Split(' ');

                        ausencia.TN_Id_Ausencia = int.Parse(dataReader["TN_Id_Ausencia"].ToString());
                        ausencia.TF_Fecha_Salida = fecha_salida[0];
                        ausencia.TF_Fecha_Regreso = fecha_regreso[0];
                        ausencia.tipoAusencia = new TipoAusencia ();
                        ausencia.tipoAusencia.TN_Id_Tipo_Ausencia = int.Parse(dataReader["TN_Id_Tipo_Ausencia"].ToString());
                        ausencia.tipoAusencia.TC_Tipo_Ausencia = dataReader["TC_Tipo_Ausencia"].ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                ausencia = null;
            }
            this.closeCon();
            return JsonConvert.SerializeObject(ausencia);
        }

        public string getAusencia(int id)
        {
            Ausencia a = new Ausencia();
            SqlDataReader dr = consultar($"EXEC sp_get_ausencia {id}");
            dr.Read();
            int aux = dr[0].ToString().IndexOf(" ");
            String aux2 = dr[0].ToString().Substring(0, aux);
            a.TF_Fecha_Salida = DateTime.Parse(aux2).ToString("dd/MM/yyyy");
            aux = dr[1].ToString().IndexOf(" ");
            aux2 = dr[1].ToString().Substring(0, aux);
            a.TF_Fecha_Regreso = DateTime.Parse(aux2).ToString("dd/MM/yyyy");
            a.TN_Id_Tipo_Ausencia = int.Parse(dr[2].ToString());
            this.closeCon();
            return JsonConvert.SerializeObject(a);
        }
    }
}