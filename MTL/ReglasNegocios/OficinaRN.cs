using AccesosDatos.Implementaciones;
using Entidad;
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
    }
}
