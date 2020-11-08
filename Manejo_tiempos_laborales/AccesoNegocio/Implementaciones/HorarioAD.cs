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
            return executing($"exec sp_registrar_horario '{horario.TC_Horario}', '{horario.TH_Duracion}'");
        }

        public int eliminarHorario(Horario horario)
        {
            return executing($"exec sp_eliminar_horario '{horario.TC_Horario}'");
        }

        public int actualizarHorario(string viejo, Horario horario){
            return executing($"exec sp_actualizar_horario '{viejo}', '{horario.TC_Horario}', '00:00:00'");
        }

        public Horario consultarTiempo(Horario horario)
        {
            Horario h = new Horario();
            SqlDataReader dataReader = consultar($"exec sp_consultar_horario '{horario.TC_Horario}', '{horario.TH_Duracion}'");

            while (dataReader.Read())
            {
                h.TC_Horario = dataReader["TC_Horario"].ToString();
                h.TH_Duracion = dataReader["TH_Duracion"].ToString();
            }
            this.closeCon();
            return h;
        }

        public string listarHorarios()
        {
            List<Horario> lista = new List<Horario>();
            SqlDataReader dataReader = consultar($"exec sp_listar_horarios");

            while (dataReader.Read())
            {
                Horario h = new Horario();
                h.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                h.TC_Horario = dataReader["TC_Horario"].ToString();
                h.TH_Duracion = dataReader["TH_Duracion"].ToString();
                lista.Add(h);
            }
            this.closeCon();
            return JsonConvert.SerializeObject(lista);
        }



       
    }
}