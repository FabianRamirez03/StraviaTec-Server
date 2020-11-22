using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIStraviaTec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data.Entity;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;


namespace APIStraviaTec.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {
        private string serverKey = Startup.getKey();
       
        [Route("deleteActivity")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarActividad([FromBody] Actividad actividad)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarActividad", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idact", NpgsqlTypes.NpgsqlDbType.Integer, actividad.Idactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("modifyActivity")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void modificarActividad([FromBody] Actividad actividad)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("modificarActividad", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idact", NpgsqlTypes.NpgsqlDbType.Integer, actividad.Idactividad);
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Nombreactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("addActivity")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Actividaddeportista agregarActividad([FromBody] Actividaddeportista actividad)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearActividad", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iddep", NpgsqlTypes.NpgsqlDbType.Integer, actividad.Iddeportista);
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Nombreactividad);
            command.Parameters.AddWithValue("@kilom", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Kilometraje);
            command.Parameters.AddWithValue("@alt", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Altura);
            command.Parameters.AddWithValue("@mapa", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Mapa);
            command.Parameters.AddWithValue("@tiempo", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Duracion);
            command.Parameters.AddWithValue("@fechaact", NpgsqlTypes.NpgsqlDbType.Date, actividad.Fecha);
            command.Parameters.AddWithValue("@tipo", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Tipoactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return actividad;
        }

        [Route("actualizarActivity")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void actualizarActividad([FromBody] Actividaddeportista actividad)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearActividad", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idact", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Idactividad);
            command.Parameters.AddWithValue("@iddep", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Iddeportista);
            command.Parameters.AddWithValue("@dist", NpgsqlTypes.NpgsqlDbType.Date, actividad.Kilometraje);
            command.Parameters.AddWithValue("@alt", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Altura);
            command.Parameters.AddWithValue("@tiempo", NpgsqlTypes.NpgsqlDbType.Date, actividad.Duracion);
            command.Parameters.AddWithValue("@mapa", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Mapa);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("ActiPorNombre")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Actividad> PostActNombre([FromBody] Actividad actividad)
        {
            List<Actividad> Activiret = new List<Actividad>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarActividadNombre", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Nombreactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    actividad.Idactividad = (int)dr[0];
                    actividad.Nombreactividad = dr[1].ToString();
                    actividad.Fecha = (DateTime)dr[2];
                    actividad.Tipoactividad = dr[3].ToString();
                    string json = JsonConvert.SerializeObject(actividad);
                    Activiret.Add(actividad);


                }

            }
            catch
            {
                Debug.WriteLine("actividad no encontrada");

            }
            conn.Close();
            return Activiret;
        }

        [Route("ActiPorId")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Actividad> PostActiId([FromBody] Actividad actividad)
        {
            List<Actividad> Activiret = new List<Actividad>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarActividadID", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idact", NpgsqlTypes.NpgsqlDbType.Integer, actividad.Idactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    actividad.Idactividad = (int)dr[0];
                    actividad.Nombreactividad = dr[1].ToString();
                    actividad.Fecha = (DateTime)dr[2];
                    actividad.Tipoactividad = dr[3].ToString();
                    string json = JsonConvert.SerializeObject(actividad);
                    Activiret.Add(actividad);


                }

            }
            catch
            {
                Debug.WriteLine("actividad no encontrada");

            }
            conn.Close();
            return Activiret;
        }

        [Route("actividadesUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> actividadesUsuario([FromBody] Usuario usuario)
        {
            List<Object> CarrerasUser = new List<Object>();
            Usuarioscarrera usuarioCarrera = new Usuarioscarrera();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("actividadesUsuarios", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            idusuario=(int)dr[0],
                            primerNombre = dr[1].ToString(),
                            idactividad = (int)dr[2],
                            nombreActividad = dr[3].ToString(),
                            tipo = dr[4].ToString(),
                            fecha = (DateTime)dr[4],
                            kilometraje = dr[6].ToString(),
                            altura = dr[7].ToString(),
                            duracion = dr[8].ToString(),
                            recorrido = dr[9].ToString()
                        }

                     };
                    Console.WriteLine(jsons);
                    CarrerasUser.Add(jsons);


                }

            }
            catch
            {
                Debug.WriteLine("Usuario no encontrado");

            }
            conn.Close();
            return CarrerasUser;
        }

        [Route("activiPorUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> activiPorUsuario([FromBody] Usuario usuario)
        {
            List<Object> CarrerasUser = new List<Object>();
            Usuarioscarrera usuarioCarrera = new Usuarioscarrera();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("actividadPorUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            IdDeportista=(int)dr[0],
                            primerNombre = dr[1].ToString(),
                            idActividad = (int)dr[2],
                            nombreActividad = dr[3].ToString(),
                            tipo = dr[4].ToString(),
                            fecha = (DateTime)dr[5],
                            kilometraje = dr[6].ToString(),
                            altura = dr[7].ToString(),
                            duracion = dr[8].ToString(),
                            recorrido = dr[9].ToString()
                        }

                     };
                    Console.WriteLine(jsons);
                    CarrerasUser.Add(jsons);


                }

            }
            catch
            {
                Debug.WriteLine("Usuario no encontrado");

            }
            List<object> retornar = new List<object>();
            for (var x = 0; x < CarrerasUser.Count; x++)
            {
                var tempList = (IList<object>)CarrerasUser[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }

    }
}
