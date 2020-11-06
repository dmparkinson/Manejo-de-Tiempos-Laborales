using AccesosDatos.Implementaciones;
using Entidad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReglasNegocios
{
    public class EmpleadoRN
    {


        public List<Empleado> ListarEmpleados()
        {
            EmpleadoAD data = new EmpleadoAD();
            string respuesta = null;
            List<Empleado> list = new List<Empleado>();
            
            try
            {
                respuesta = data.listarEmpleados();
                if (respuesta != null)
                {
                    list= JsonConvert.DeserializeObject<List<Empleado>>(respuesta);
                }
                

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return list;
        }

        public List<Oficina> listarOficinasEmpleado()
        {
            EmpleadoAD data = new EmpleadoAD();
            string respuesta = null;
            List<Oficina> list = new List<Oficina>();

            try
            {
                respuesta = data.listarOficinasEmpleado();
                if (respuesta != null)
                {
                    list = JsonConvert.DeserializeObject<List<Oficina>>(respuesta);
                }


                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return list;
        }
            

        public List<Puesto> listarPuestosEmpleado()
        {
            EmpleadoAD data = new EmpleadoAD();
            string respuesta = null;
            List<Puesto> list = new List<Puesto>();

            try
            {
                respuesta = data.listarPuestosEmpleado();
                if (respuesta != null)
                {
                    list = JsonConvert.DeserializeObject<List<Puesto>>(respuesta);
                }


                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return list;
        }

        public int Insertar(string usuario, string password, string cedula, string nombre, string apUno, string apDos, string correo, string tipo, string estado, int puesto, int oficina)
        {
            EmpleadoAD data = new EmpleadoAD();
            int respuesta = 0;
            try
            {
                Empleado e = new Empleado();
                e.TC_Usuario = usuario;
                e.TC_Contrasena = password;
                e.TC_Identificacion = cedula;
                e.TC_Nombre_Usuario = nombre;
                e.TC_Primer_Apellido = apUno;
                e.TC_Segundo_Apellido = apDos;
                e.TC_Correo = correo;
                e.TC_Tipo_Usuario = tipo;
                e.TB_Activo = int.Parse(estado);
                e.TN_Id_Puesto = puesto;
                e.TN_Id_Oficina = oficina;
                respuesta = data.registrarEmpleado(e);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error Insertando Empleado");
            }
            return respuesta;
        }

        public int Eliminar(int id)
        {
            EmpleadoAD data = new EmpleadoAD();
            int respuesta = 0;
            try
            {
                respuesta = data.eliminarEmpleado(id);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error eliminando Empleado");
            }
            return respuesta;
        }

        public int Actualizar(int id, string cedula, string nombre, string apUno, string apDos, string correo, string tipo, string estado, int puesto, int oficina)
        {
            EmpleadoAD data = new EmpleadoAD();
            int respuesta = 0;
            try
            {
                Empleado e = new Empleado();
                e.TN_Id_Usuario = id;
                e.TC_Identificacion = cedula;
                e.TC_Nombre_Usuario = nombre;
                e.TC_Primer_Apellido = apUno;
                e.TC_Segundo_Apellido = apDos;
                e.TC_Correo = correo;
                e.TC_Tipo_Usuario = tipo;
                e.TB_Activo = int.Parse(estado);
                e.TN_Id_Puesto = puesto;
                e.TN_Id_Oficina = oficina;
                respuesta = data.actualizarEmpleado(e);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error actualizando Empleado");
            }
            return respuesta;
        }


        // Obtener un empleado  solicitado
        public string ObtenerEmpleado(Empleado _empleado)
        {
            EmpleadoAD empleadoAD = new EmpleadoAD();
            string respuesta = null;

            //Llamado a accedo de datos
            try
            {
                respuesta = empleadoAD.getEmpleado(_empleado); // Resultado de la operacion

                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return respuesta;
        }
    }
}
