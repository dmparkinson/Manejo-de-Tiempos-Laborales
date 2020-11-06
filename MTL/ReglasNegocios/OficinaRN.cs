using AccesosDatos.Implementaciones;
using Entidad;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class OficinaRN
    {
        public List<Oficina> listarOficinas(string nombre)
        {
            OficinaAD data = new OficinaAD();
            string respuesta = null;
            List<Oficina> list = new List<Oficina>();
            try
            {
                respuesta = data.listarOficinas(nombre);
                if (respuesta != null)
                {
                    list = JsonConvert.DeserializeObject<List<Oficina>>(respuesta);
                }
            }
            catch (Exception e)
            {

            }
            return list;
        }
        public List<Circuito> listarCircuitosOficinas()
        {
            OficinaAD data = new OficinaAD();
            string respuesta = null;
            List<Circuito> list = new List<Circuito>();
            try
            {
                respuesta = data.listarCircuitosOficinas();
                if (respuesta != null)
                {
                    list = JsonConvert.DeserializeObject<List<Circuito>>(respuesta);
                }
            }
            catch (Exception e)
            {

            }
            return list;
        }

        public int insertarOficina(string codigo, string nombre, int circuito, int activo, string fechai, string fechaf)
        {
            OficinaAD data = new OficinaAD();
            int respuesta = -1;
            try
            {
                Oficina a = new Oficina();
                a.TC_Codigo = codigo;
                a.TC_Nombre_Oficina = nombre;
                a.TN_Id_Circuito = circuito;
                a.TB_Activa = activo;
                a.TF_Inicio_Vigencia = fechai;
                a.TF_Fin_Vigencia = fechaf;
                respuesta = data.registrarOficina(a);
            }
            catch (Exception e)
            {
                respuesta = -1;
            }
            return respuesta;
        }

        public int actualizarOficina(int id, string codigo, string nombre, int circuito, int activo, string fechai, string fechaf)
        {
            OficinaAD data = new OficinaAD();
            int respuesta = -1;
            try
            {
                Oficina a = new Oficina();
                a.TN_Id_Oficina = id;
                a.TC_Codigo = codigo;
                a.TC_Nombre_Oficina = nombre;
                a.TN_Id_Circuito = circuito;
                a.TB_Activa = activo;
                a.TF_Inicio_Vigencia = fechai;
                a.TF_Fin_Vigencia = fechaf;
                respuesta = data.actualizarOficina(a);
            }
            catch (Exception e)
            {
                respuesta = -1;
            }
            return respuesta;
        }

        public int eliminarOficina(int id)
        {
            OficinaAD data = new OficinaAD();
            int respuesta = -1;
            try
            {
                respuesta = data.eliminarOficina(id);
            }
            catch (Exception e)
            {
                respuesta = -1;
            }
            return respuesta;
        }
    }
}
