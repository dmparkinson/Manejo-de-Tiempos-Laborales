﻿using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AccesoDatos.Implementaciones
{
    public class TiemposAD : ConexionAD.ConexionAD
    {
        public int registrarTiempo(Tiempo tiempo) {
            return executing($"exec sp_registrar_tiempo_usuario '{tiempo.TC_Tipo}', {tiempo.TN_Id_Horario}, {tiempo.TN_Id_Usuario}");
        }

        public int eliminarTiempo(int idTiempo){
            return executing($"exec sp_eliminar_tiempo_usuario {idTiempo}");
        }

        public int actualizarTiempo(Tiempo tiempo)
        {
            return executing($"exec sp_eliminar_tiempo_usuario '{tiempo.TC_Tipo}', {tiempo.TN_Id_Tiempo}, {tiempo.TN_Id_Usuario}");
        }

        public int actualizarTiempoHistorico(Tiempo tiempo) {
            //@TN_ID_TIEMPO INT, @TN_ID_HORARIO INT, @TC_TIPO VARCHAR(1), @TF_FECHA DATE, @TH_HORA TIME
            return executing($"exec sp_actualizar_tiempo_usuario_historico {tiempo.TN_Id_Tiempo}, {tiempo.TN_Id_Horario}, '{tiempo.TC_Tipo}', '{tiempo.TF_Fecha}', '{tiempo.TH_Hora}'");
        }

        //lista todos los tiempos de un solo usuario
        public string consultarTiemposUsuario(int idEmpleado)
        {
            List<Tiempo> lista = new List<Tiempo>();
            SqlDataReader dataReader = consultar($"exec sp_consultar_todo_tiempo_usuario {idEmpleado}");

            while (dataReader.Read())
            {
                Tiempo t = new Tiempo();
                string[] fechas = dataReader["TF_Fecha"].ToString().Split(' ');
                t.TN_Id_Tiempo = int.Parse(dataReader["TN_Id_Tiempo"].ToString());
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                t.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                t.TF_Fecha = fechas[0];
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                t.TC_Horario = dataReader["TC_Horario"].ToString();
                lista.Add(t);
            }
            this.closeCon();
            return JsonConvert.SerializeObject(lista);
        }


        //lista un solo tiempo de un solo usuario
        public string consultarTiempoUsuario(Tiempo tiempo)
        {
            Tiempo t = new Tiempo();
            SqlDataReader dataReader = consultar($"exec sp_consultar_tiempo_usuario {tiempo.TN_Id_Tiempo}");

            while (dataReader.Read())
            {
                t.TN_Id_Tiempo = int.Parse(dataReader["TN_Id_Tiempo"].ToString());
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                t.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                t.TC_Horario = dataReader["TC_Horario"].ToString();
                t.TF_Fecha = dataReader["TF_Fecha"].ToString();
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
            }
            this.closeCon();
            return JsonConvert.SerializeObject(t);
        }


        //lista todos los tiempos de todos los usuarios
        public string listarTiempoUsuario()
        {
            List<Tiempo> lista = new List<Tiempo>();
            SqlDataReader dataReader = consultar($"exec sp_listar_tiempo_usuario");

            while (dataReader.Read())
            {
                Tiempo t = new Tiempo();
                string[] fechas = dataReader["TF_Fecha"].ToString().Split(' ');
                t.TF_Fecha = fechas[0];
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                t.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                t.empleado = new Empleado();
                t.empleado.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                t.empleado.TC_Usuario = dataReader["TC_Usuario"].ToString();
                t.empleado.TC_Contrasena = dataReader["TC_Contrasena"].ToString();
                t.empleado.TC_Identificacion = dataReader["TC_Identificacion"].ToString();
                t.empleado.TC_Nombre_Usuario = dataReader["TC_Nombre_Usuario"].ToString();
                t.empleado.TC_Primer_Apellido = dataReader["TC_Primer_Apellido"].ToString();
                t.empleado.TC_Segundo_Apellido = dataReader["TC_Segundo_Apellido"].ToString();
                t.empleado.TC_Tipo_Usuario = dataReader["TC_Tipo_Usuario"].ToString();
                t.empleado.TC_Correo = dataReader["TC_Correo"].ToString();
                t.empleado.TC_Usuario = dataReader["TC_Usuario"].ToString();
                t.empleado.TB_Activo = int.Parse(dataReader["TB_Activo"].ToString());
                t.empleado.TN_Id_Puesto = int.Parse(dataReader["TN_Id_Puesto"].ToString());
                t.empleado.TN_Id_Oficina = int.Parse(dataReader["TN_Id_Oficina"].ToString());
                t.empleado.TB_Eliminado = int.Parse(dataReader["TB_Eliminado"].ToString());

                lista.Add(t);
            }
            this.closeCon();
            return JsonConvert.SerializeObject(lista);
        }

        //lista los tiempos de un usuario en una fecha específica
        public string listarPorFechaTiempoUsuario(int idEmpleado, string fechaActual) {
            List<Tiempo> lista = new List<Tiempo>();
            SqlDataReader dataReader = consultar($"exec sp_listar_tiempo_usuario_byFecha {idEmpleado}, '{fechaActual}'");

            while (dataReader.Read())
            {
                Tiempo t = new Tiempo();
                string[] fechas = dataReader["TF_Fecha"].ToString().Split(' ');
                t.TN_Id_Tiempo = int.Parse(dataReader["TN_Id_Tiempo"].ToString());
                t.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                t.TF_Fecha = fechas[0];
                t.TH_Hora = dataReader["TH_Hora"].ToString();
                t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                t.TC_Horario = dataReader["TC_Horario"].ToString();

                lista.Add(t);
            }

            this.closeCon();
            return JsonConvert.SerializeObject(lista);
        }




        public string listarHistoricoHorarios()
        {
            List<Tiempo> lista = new List<Tiempo>();
            
            try {
                SqlDataReader dataReader = consultar($"exec sp_historico_tiempos");
                while (dataReader.Read())
                {
                    Tiempo t = new Tiempo();
                    string[] fechas = dataReader["TF_Fecha"].ToString().Split(' ');
                    t.TF_Fecha = fechas[0];
                    t.TH_Hora = dataReader["TH_Hora"].ToString();
                    t.TC_Tipo = dataReader["TC_Tipo"].ToString();
                    t.TN_Id_Tiempo = int.Parse(dataReader["TN_Id_Tiempo"].ToString());
                    t.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                    t.TN_Id_Usuario = int.Parse(dataReader["TN_Id_Usuario"].ToString());
                    t.empleado = new Empleado();
                    t.empleado.TC_Identificacion = dataReader["TC_Identificacion"].ToString();
                    t.empleado.TC_Nombre_Usuario = dataReader["TC_Nombre_Usuario"].ToString();
                    t.empleado.TC_Primer_Apellido = dataReader["TC_Primer_Apellido"].ToString();
                    t.empleado.TC_Segundo_Apellido = dataReader["TC_Segundo_Apellido"].ToString();
                    t.horario = new Horario();
                    t.horario.TN_Id_Horario = int.Parse(dataReader["TN_Id_Horario"].ToString());
                    t.horario.TC_Horario = dataReader["TC_Horario"].ToString();
                    t.TC_Horario = dataReader["TC_Horario"].ToString();

                    lista.Add(t);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            this.closeCon();
            return JsonConvert.SerializeObject(lista);
        }

    }
}