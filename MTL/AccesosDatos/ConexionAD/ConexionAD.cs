using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesosDatos.ConexionAD
{
    public class ConexionAD
    {
        private string conexion;
        private SqlConnection con;
        public ConexionAD()
        {
            conexion = "Data Source=163.178.107.10;Initial Catalog=Manejo_Tiempos_Laborales;Persist Security Info=True;User ID=laboratorios;Password=KmZpo.2796";
        }

        public void conectar()
        {
            con = new SqlConnection(conexion);

            if (con != null)
            {
                Console.WriteLine("Conexion establecida");
            }
            else
            {
                Console.WriteLine("Conexion Erronea");
            }

        }

        public SqlDataReader consultar(string sentencia)
        {
            SqlDataReader dr = null;
            try
            {
                conectar();
                con.Open();
                SqlCommand cmd = new SqlCommand(sentencia, con);
                dr = cmd.ExecuteReader();
            }
            catch
            {
                con.Close();
                Console.WriteLine("Error en consultar");
            }
            return dr;
        }

        public int ejecutar(string sentencia)
        {
            int salida = 0;
            try
            {
                conectar();
                con.Open();
                SqlCommand cmd = new SqlCommand(sentencia, con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Ejecución exitosa");
                salida = 1;
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error en ejecutar " + e);
                salida = 0;
                con.Close();
            }
            return salida;
        }
    }
}