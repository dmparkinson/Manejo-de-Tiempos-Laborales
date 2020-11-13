using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Implementaciones
{
    public class OficinaAD:ConexionAD.ConexionAD
    {

        public string listarOficinas(string nombre)
        {
            List<Oficina> list = new List<Oficina>();
            try {
                SqlDataReader dr = consultar($"exec sp_lista_completa_oficinas '%{nombre}%'");
                while (dr.Read())
                {
                    Oficina a = new Oficina();
                    a.TN_Id_Oficina = int.Parse(dr[0].ToString());
                    a.TC_Codigo = dr[1].ToString();
                    a.TC_Nombre_Oficina = dr[2].ToString();
                    a.TC_Desc_Circuito = dr[3].ToString();
                    a.TB_Activa = int.Parse(dr[4].ToString());
                    int aux = dr[5].ToString().IndexOf(" ");
                    String aux2 = dr[5].ToString().Substring(0, aux);
                    a.TF_Inicio_Vigencia = DateTime.Parse(aux2).ToString("dd-MM-yyyy");
                    aux = dr[6].ToString().IndexOf(" ");
                    aux2 = dr[6].ToString().Substring(0, aux);
                    a.TF_Fin_Vigencia = DateTime.Parse(aux2).ToString("dd-MM-yyyy");
                    list.Add(a);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return JsonConvert.SerializeObject(list);
        }

        public string listarCircuitosOficinas()
        {
            List<Circuito> list = new List<Circuito>();
            try
            {
                SqlDataReader dr = consultar($"exec sp_listar_Circuitos");
                while (dr.Read())
                {
                    Circuito c = new Circuito();
                    c.TN_Id_Circuito = int.Parse(dr[0].ToString());
                    c.TC_Desc_Circuito = dr[1].ToString();
                    list.Add(c);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return JsonConvert.SerializeObject(list);
        }

        public int registrarOficina(Oficina a)
        {
            int salida = -1;
            try {
                SqlDataReader dr = consultar($"EXEC sp_registrar_oficina '{a.TC_Codigo}','{a.TC_Nombre_Oficina}',{a.TN_Id_Circuito},{a.TB_Activa},'{a.TF_Inicio_Vigencia}','{a.TF_Fin_Vigencia}'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());


            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return salida;
        }



        public int actualizarOficina(Oficina a)
        {
            int salida = -1;
            try {
                SqlDataReader dr = consultar($"EXEC sp_actualizar_oficina {a.TN_Id_Oficina},'{a.TC_Codigo}','{a.TC_Nombre_Oficina}',{a.TN_Id_Circuito},{a.TB_Activa},'{a.TF_Inicio_Vigencia}','{a.TF_Fin_Vigencia}'");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
                
            }
            catch (SqlException e) {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return salida;
        }



        public int eliminarOficina(int id)
        {
            int salida = -1;
            try {
                SqlDataReader dr = consultar($"EXEC sp_delete_oficina {id}");
                dr.Read();
                salida = int.Parse(dr[0].ToString());
            } catch (SqlException e) {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return salida;
        }



        public string getOficina(int id)
        {
            Oficina a = new Oficina();
            try {
                SqlDataReader dr = consultar($" EXEC sp_get_oficina {id}");
                dr.Read();
                a.TC_Codigo = dr[0].ToString();
                a.TC_Nombre_Oficina = dr[1].ToString();
                a.TN_Id_Circuito = int.Parse(dr[2].ToString());
                a.TB_Activa = int.Parse(dr[3].ToString());
                int aux = dr[4].ToString().IndexOf(" ");
                String aux2 = dr[4].ToString().Substring(0, aux);
                a.TF_Inicio_Vigencia = DateTime.Parse(aux2).ToString("dd-MM-yyyy");
                aux = dr[5].ToString().IndexOf(" ");
                aux2 = dr[5].ToString().Substring(0, aux);
                a.TF_Fin_Vigencia = DateTime.Parse(aux2).ToString("dd-MM-yyyy");
                
            } catch (SqlException e) {
                Console.WriteLine(e.ToString());
            }
            this.closeCon();
            return JsonConvert.SerializeObject(a);
        }

    }
}
