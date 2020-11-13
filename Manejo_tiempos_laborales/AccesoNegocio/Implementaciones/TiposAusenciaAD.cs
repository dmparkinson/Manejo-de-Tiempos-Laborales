using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace AccesoDatos.Implementaciones
{
    public class TiposAusenciaAD : ConexionAD.ConexionAD
    {

        
        // Listar todos los tipos de ausencia en el sistema
        public string listarTiposAusencia()
        {
            List<TipoAusencia> lista = new List<TipoAusencia>();
            SqlDataReader dataReader = consultar($"exec sp_listar_Tipo_Ausencias");
            while (dataReader.Read())
            {
                TipoAusencia tAusencia = new TipoAusencia();
                tAusencia.TN_Id_Tipo_Ausencia = int.Parse(dataReader["TN_Id_Tipo_Ausencia"].ToString());
                tAusencia.TC_Tipo_Ausencia = dataReader["TC_Tipo_Ausencia"].ToString();
                lista.Add(tAusencia);
            }
            this.closeCon();
            return JsonConvert.SerializeObject(lista);
            
        }




        // Obtener un tipo de ausencia
        public string getTipoAusencia(TipoAusencia _tAusencia)
        {
            TipoAusencia tAusencia = null;
            try
            {
                SqlDataReader dr = consultar("EXEC get_tipo_ausencia '" + _tAusencia.TN_Id_Tipo_Ausencia + "'");
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        tAusencia = new TipoAusencia();
                        tAusencia.TN_Id_Tipo_Ausencia = int.Parse(dr["TN_Id_Tipo_Ausencia"].ToString());
                        tAusencia.TC_Tipo_Ausencia = dr["TC_Tipo_Ausencia"].ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                tAusencia = null;
            }
            this.closeCon();
            return JsonConvert.SerializeObject(tAusencia);
        }





        // Registrar un nuevo tipo de ausencia
        public int insertTiposAusencia(TipoAusencia _tAusencia)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar("EXEC insert_tipo_ausencia '" + _tAusencia.TC_Tipo_Ausencia + "'");
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




        // Actualizar los datos de un tipo de ausencia
        public int updateTiposAusencia(TipoAusencia _tAusencia)
        {
            int salida = 0;

            try
            {
                SqlDataReader dr = consultar("EXEC update_tipo_ausencia '" + _tAusencia.TC_Tipo_Ausencia + "', '" +_tAusencia.TN_Id_Tipo_Ausencia + "'");
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




        // Eliminar un tipo de ausencia
        public int deleteTipoAusencia(TipoAusencia _tAusencia)
        {
            int salida = 0;
            try
            {
                SqlDataReader dr = consultar($"EXEC delete_tipo_ausencia '{_tAusencia.TN_Id_Tipo_Ausencia }'");
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