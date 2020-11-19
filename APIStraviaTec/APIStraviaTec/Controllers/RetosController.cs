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
    public class RetosController : ControllerBase
    {
        private string serverKey = Startup.getKey();
        [Route("retosPorUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> retosPorUsuario([FromBody] Usuario usuario)
        {
            List<Object> RetosUser = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("retosPorUsuario", conn);
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
                            nombreUsuario = dr[1].ToString(),
                            idReto = (int)dr[2],
                            nombreReto = dr[3].ToString(),
                            objetivo = dr[4].ToString(),
                            tipoActividad = dr[5].ToString(),
                            tipoReto = dr[6].ToString(),
                            fechaInicio = (DateTime)dr[7],
                            fechaFinal = (DateTime)dr[8],
                            kilometraje = dr[9].ToString(),
                            altura = dr[10].ToString(),
                            duracion = dr[11].ToString(),
                            completitud = (bool)dr[12],
                            recorrido = dr[13].ToString()
                        }

                     };
                    Console.WriteLine(jsons);
                    RetosUser.Add(jsons);


                }

            }
            catch
            {
                Debug.WriteLine("Reto no encontrado");

            }
            conn.Close();
            List<object> retornar = new List<object>();
            for (var x = 0; x < RetosUser.Count; x++)
            {
                var tempList = (IList<object>)RetosUser[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }

        [Route("AddPatrocinador")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void modificarActividad([FromBody] Patrocinadoresreto reto)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarPatrocinadorReto", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idreto);
            command.Parameters.AddWithValue("@nombrepatrocinador", NpgsqlTypes.NpgsqlDbType.Varchar, reto.Nombrecomercial);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("addUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarUsuarioReto([FromBody] Usuariosreto user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarUsuarioReto", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iddep", NpgsqlTypes.NpgsqlDbType.Integer, user.Iddeportista);
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, user.Idreto);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("deleteUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarUsuario([FromBody] Usuariosreto user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarUsuarioReto", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iddep", NpgsqlTypes.NpgsqlDbType.Integer, user.Iddeportista);
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, user.Idreto);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("updateUserRetos")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void actualizarDatosRetoUsuario(Usuariosreto reto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("actualizarDatosRetoUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idreto);
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, reto.Iddeportista);
            command.Parameters.AddWithValue("@tiempo", NpgsqlTypes.NpgsqlDbType.Text, reto.Duracion);
            command.Parameters.AddWithValue("@distancia", NpgsqlTypes.NpgsqlDbType.Text, reto.Kilometraje);
            command.Parameters.AddWithValue("@alt", NpgsqlTypes.NpgsqlDbType.Text, reto.Altura);
            command.Parameters.AddWithValue("@completado", NpgsqlTypes.NpgsqlDbType.Boolean, reto.Completitud);
            command.Parameters.AddWithValue("@mapa", NpgsqlTypes.NpgsqlDbType.Text, reto.Recorrido);
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("deleteReto")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarReto([FromBody] Reto reto)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarRetoID", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idreto);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("updateReto")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void modificarReto(Reto reto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("modificarReto", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idreto);
            command.Parameters.AddWithValue("@idorga", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idorganizador);
            command.Parameters.AddWithValue("@nombreto", NpgsqlTypes.NpgsqlDbType.Text, reto.Nombrereto);
            command.Parameters.AddWithValue("@obj", NpgsqlTypes.NpgsqlDbType.Text, reto.Objetivoreto);
            command.Parameters.AddWithValue("@fechainc", NpgsqlTypes.NpgsqlDbType.Date, reto.Fechainicio);
            command.Parameters.AddWithValue("@fechafin", NpgsqlTypes.NpgsqlDbType.Date, reto.Fechafinaliza);
            command.Parameters.AddWithValue("@tipoact", NpgsqlTypes.NpgsqlDbType.Text, reto.Tipoactividad);
            command.Parameters.AddWithValue("@tiporet", NpgsqlTypes.NpgsqlDbType.Text, reto.Tiporeto);
            command.Parameters.AddWithValue("@privacidad", NpgsqlTypes.NpgsqlDbType.Boolean, reto.Privada);
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("addReto")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearReto(Reto reto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearReto", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idorga", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idorganizador);
            command.Parameters.AddWithValue("@nombreto", NpgsqlTypes.NpgsqlDbType.Text, reto.Nombrereto);
            command.Parameters.AddWithValue("@obj", NpgsqlTypes.NpgsqlDbType.Text, reto.Objetivoreto);
            command.Parameters.AddWithValue("@fechainc", NpgsqlTypes.NpgsqlDbType.Date, reto.Fechainicio);
            command.Parameters.AddWithValue("@fechafin", NpgsqlTypes.NpgsqlDbType.Date, reto.Fechafinaliza);
            command.Parameters.AddWithValue("@tipoact", NpgsqlTypes.NpgsqlDbType.Text, reto.Tipoactividad);
            command.Parameters.AddWithValue("@tiporet", NpgsqlTypes.NpgsqlDbType.Text, reto.Tiporeto);
            command.Parameters.AddWithValue("@privacidad", NpgsqlTypes.NpgsqlDbType.Boolean, reto.Privada);
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
    }
}
