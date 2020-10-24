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
    public class TiempoRN
    {
        public TiempoRN() {}
        public int verificarRegistro(string tiempo, int idUser)
        {
            TiemposAD data = new TiemposAD();
            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            //primero debemos hacer una lista de los registros que hay
            string lista = data.listarPorFechaTiempoUsuario(idUser, fecha);

            //hacemos la lista de tiempos
            List<Tiempo> listaT = JsonConvert.DeserializeObject<List<Tiempo>>(lista);

            //si la lista está vacía y el nuevo registro es entrada retorna 1
            if (listaT.Count == 0) {
                if (tiempo.Equals("Entrada")){
                    return 1;
                }else {
                    return -1;
                }
            }

            //en caso de que la lista esté llena
            if (listaT.Count>0) {
                for (int i=0; i<listaT.Count; i++) {
                    //si la salida del trabajo fue marcada, entonces no se pueden realizar más registros
                    if (listaT[i].TC_Horario.Equals("Salida")) {
                        return -2;
                    }

                    //verificamos si la marca de tiempo ya existe
                    if (listaT[i].TC_Horario.Equals(tiempo)){
                        return -3;
                    }
                }

                //se debe verificar que la marca anterior se haya cerrado para realizar otra de tipo entrada
                int q = listaT.Count - 1;
                if (!listaT[q].TC_Horario.Equals("Entrada")) {
                    if (listaT[q].TC_Tipo.Equals("E") && (tiempo.ToCharArray()[0].Equals('E') || tiempo.Equals("Salida"))) {
                        return -4;
                    }
                }

                //se debe verificar que para marcar una salida, ya exista una entrada
                if (!tiempo.Equals("Entrada") && !tiempo.Equals("Salida"))
                {
                    if (tiempo.ToCharArray()[0].Equals('S'))
                    {
                        string[] nombreTipo = tiempo.Split(' ');
                        string temp = "";
                        for (int i = 1; i < nombreTipo.Length; i++)
                        {
                            temp = temp + " " + nombreTipo[i];
                        }

                        temp = "Entrada" + temp;

                        for (int i = 0; i < listaT.Count; i++)
                        {
                            if (listaT[i].TC_Horario.Equals(temp))
                            {
                                return 1;
                            }
                        }
                        return -5;
                    }
                }
            }

            return 1;
        }
    }
}
