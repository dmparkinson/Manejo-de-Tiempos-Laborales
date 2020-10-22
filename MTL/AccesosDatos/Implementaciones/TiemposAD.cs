using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesosDatos.Implementaciones
{
    public class TiemposAD : ConexionAD.ConexionAD
    {
        public int registrarTiempo(Tiempo tiempo) {
            return ejecutar($"exec sp_registrar_tiempo_usuario '{tiempo.TC_Tipo}', '{tiempo.TC_Horario}', {tiempo.TN_Id_Usuario}");
        }

        public int eliminarTiempo(int idTiempo){
            return ejecutar($"exec sp_eliminar_tiempo_usuario {idTiempo}");
        }

        public int actualizarTiempo(Tiempo tiempo)
        {
            return ejecutar($"exec sp_eliminar_tiempo_usuario '{tiempo.TC_Tipo}', '{tiempo.TC_Horario}', {tiempo.TN_Id_Usuario}");
        }

        //lista todos los tiempos de un solo usuario
        public string consultarTiemposUsuario(int idEmpleado)
        {
            List<Tiempo> lista = new List<Tiempo>();
            SqlDataReader dataReader = consultar($"exec sp_consultar_todo_tiempo_usuario {idEmpleado}");

            while (dataReader.Read())
            {
                Tiempo t = new Tiempo();
                t.TF_Fecha = dataReader["TF_Fecha"].ToString();
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                t.TC_Horario = dataReader["TC_Horario"].ToString();
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                lista.Add(t);
            }

            return JsonConvert.SerializeObject(lista);
        }


        //lista un solo tiempo de un solo usuario
        public string consultarTiempoUsuario(Tiempo tiempo)
        {
            Tiempo t = new Tiempo();
            SqlDataReader dataReader = consultar($"exec sp_consultar_tiempo_usuario '{tiempo.TF_Fecha}', '{tiempo.TC_Horario}', {tiempo.TN_Id_Usuario}");

            while (dataReader.Read())
            {
                t.TF_Fecha = dataReader["TF_Fecha"].ToString();
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                t.TC_Horario = dataReader["TC_Horario"].ToString();
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
            }

            return JsonConvert.SerializeObject(t);
        }


        //lista todos los tiempos de todos los usuarios
        public string listarTiempoUsuario(Tiempo tiempo)
        {
            List<Tiempo> lista = new List<Tiempo>();
            SqlDataReader dataReader = consultar($"exec sp_listar_tiempo_usuario");

            while (dataReader.Read())
            {
                Tiempo t = new Tiempo();
                t.TF_Fecha = dataReader["TF_Fecha"].ToString();
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                t.TC_Horario = dataReader["TC_Horario"].ToString();
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());

                lista.Add(t);
            }

            return JsonConvert.SerializeObject(lista);
        }
    }
}