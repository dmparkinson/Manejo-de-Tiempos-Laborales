using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesosDatos
{
    public class CircuitoAD : ConexionAD.ConexionAD
    {


        // Listar todos los circuitos del sistema
        public string listarCircuito()
        {
            List<Circuito> lista = new List<Circuito>();
            SqlDataReader dataReader = consultar($"exec sp_listar_Circuitos");
            while (dataReader.Read())
            {
                Circuito circuito = new Circuito();
                circuito.TN_Id_Circuito = int.Parse(dataReader["TN_Id_Circuito"].ToString());
                circuito.TC_Desc_Circuito = dataReader["TC_Desc_Circuito"].ToString();
                lista.Add(circuito);
            }
            return JsonConvert.SerializeObject(lista);

        }




        // Obtener un circuito
        public string getCircuito(Circuito _circuito)
        {
            Circuito circuito = null;
            try
            {
                SqlDataReader dr = consultar("EXEC get_circuito '" + _circuito.TN_Id_Circuito + "'");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        circuito = new Circuito();
                        circuito.TN_Id_Circuito = int.Parse(dr["TN_Id_Circuito"].ToString());
                        circuito.TC_Desc_Circuito = dr["TC_Desc_Circuito"].ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                circuito = null;
            }
            return JsonConvert.SerializeObject(circuito);
        }





        // Registrar un nuevo circuito
        public int insertCircuito(Circuito _circuito)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar("EXEC sp_registrar_circuito '" + _circuito.TC_Desc_Circuito+ "'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }
            return salida;
        }




        // Actualizar los datos de un circuito
        public int updateCircuito(Circuito _circuito)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC sp_update_circuito '" + _circuito.TC_Desc_Circuito + "', '" + _circuito.TN_Id_Circuito+ "'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            }
            catch (SqlException e)
            {
                salida = -1;
            }

            return salida;
        }




        // Eliminar un circuito
        public int deleteCircuito(Circuito _circuito)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar($"EXEC sp_delete_circuito'{_circuito.TN_Id_Circuito}'");
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
