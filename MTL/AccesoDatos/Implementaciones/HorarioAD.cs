using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesoDatos.Implementaciones
{
    public class HorarioAD : ConexionAD.ConexionAD
    {

        public int registrarHorario(Horario horario) {
            return ejecutar($"exec sp_registrar_horario '{horario.TC_Horario}', {horario.TH_Duracion}");
        }

        public int eliminarHorario(Horario horario)
        {
            return ejecutar($"exec sp_eliminar_horario '{horario.TC_Horario}'");
        }

        public string consultarHorario(Horario horario) {
            Horario h = new Horario();
            SqlDataReader dataReader = consultar($"exec sp_consultar_horario '{horario.TC_Horario}', {horario.TH_Duracion}");

            while (dataReader.Read())
            {
                h.TC_Horario = dataReader["TC_Horario"].ToString();
                h.TH_Duracion = int.Parse(dataReader["TH_Duracion"].ToString());
            }

            return JsonConvert.SerializeObject(h);
        }

        public string listarHorarios(Horario horario)
        {
            List<Horario> lista = new List<Horario>();
            SqlDataReader dataReader = consultar($"exec sp_listar_horarios");

            while (dataReader.Read())
            {
                Horario h = new Horario();
                h.TC_Horario = dataReader["TC_Horario"].ToString();
                h.TH_Duracion = int.Parse(dataReader["TH_Duracion"].ToString());
                lista.Add(h);
            }

            return JsonConvert.SerializeObject(lista);
        }

    }
}