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
    public class CarreraController : ControllerBase
    {
        private string serverKey = Startup.getKey();
        [Route("create")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void crearCarrera([FromBody] Carrera carrera)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idorga", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Idorganizador);
            command.Parameters.AddWithValue("@nombcarr", NpgsqlTypes.NpgsqlDbType.Date, carrera.Nombrecarrera);
            command.Parameters.AddWithValue("@fechaevento", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Fechacarrera);
            command.Parameters.AddWithValue("@tipocarrera", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Tipoactividad);
            command.Parameters.AddWithValue("@recorridocarrera", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Recorrido);
            command.Parameters.AddWithValue("@privacidad", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Privada);
            command.Parameters.AddWithValue("@precio", NpgsqlTypes.NpgsqlDbType.Date, carrera.Costo);
            command.Parameters.AddWithValue("@cuentabanc", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Cuentabancaria);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("categoria")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void categoriaCarrera([FromBody] Carrera carrera)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("asignarCategoriaCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Idcarrera);
            command.Parameters.AddWithValue("@categ", NpgsqlTypes.NpgsqlDbType.Date, carrera.Categoria);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("CarreraID")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Carrera> PostActId([FromBody] Carrera carrera)
        {
            List<Carrera> Activiret = new List<Carrera>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("obteneridcarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Nombrecarrera);
            try
            {
                carrera.Idcarrera = (int)command.ExecuteScalar(); ;

            }
            catch
            {
                Debug.WriteLine("carrera no encontrada");

            }
            conn.Close();
            return Activiret;
        }

        [Route("modifCarrera")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void modificarCarrera([FromBody] Carrera carrera)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("modificarCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idcarrera);
            command.Parameters.AddWithValue("@idorg", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idorganizador);
            command.Parameters.AddWithValue("@nomcarr", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Idcarrera);
            command.Parameters.AddWithValue("@fechcarr", NpgsqlTypes.NpgsqlDbType.Timestamp, carrera.Idorganizador);
            command.Parameters.AddWithValue("@tipact", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Tipoactividad);
            command.Parameters.AddWithValue("@ruta", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Recorrido);
            command.Parameters.AddWithValue("@privacidad", NpgsqlTypes.NpgsqlDbType.Boolean, carrera.Privada);
            command.Parameters.AddWithValue("@precio", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Costo);
            command.Parameters.AddWithValue("@cuenta", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Cuentabancaria);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("AddPatrocinador")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void modificarActividad([FromBody] Patrocinadorescarrera carrera)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarPatrocinadorCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idcarrera);
            command.Parameters.AddWithValue("@nombrepatrocinador", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Nombrecomercial);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("delete")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarActividad([FromBody] Carrera carrera)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarCarreraID", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idcarrera);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }


        [Route("addUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarUsuario([FromBody] Usuarioscarrera user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarUsuarioCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iddep", NpgsqlTypes.NpgsqlDbType.Integer, user.Iddeportista);
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, user.Idcarrera);
            command.Parameters.AddWithValue("@catCarr", NpgsqlTypes.NpgsqlDbType.Integer, user.Categoriacompite);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("participantesCarrera")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> ParticipantesCarrera([FromBody] Carrera carrera)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            Usuario usuario = new Usuario();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("participantesCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idcarrera);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    usuario.Primernombre = dr[0].ToString();
                    usuario.Apellidos = dr[1].ToString();
                    usuario.Edad = (int)dr[2];
                    usuario.Categoria = dr[3].ToString();
                    string json = JsonConvert.SerializeObject(usuario);
                    Usuarioret.Add(usuario);


                }

            }
            catch
            {
                Debug.WriteLine("Usuario no encontrado");

            }
            conn.Close();
            return Usuarioret;
        }

        [Route("carrerasPorUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> CarrerasPorUsuario([FromBody] Usuario usuario)
        {
            List<Object> CarrerasUser = new List<Object>();
            Usuarioscarrera usuarioCarrera = new Usuarioscarrera();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarCarrerasPorUsuaio", conn);
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
                            apellido = dr[2].ToString(),
                            categoria = dr[3].ToString(),
                            idCarrera = (int)dr[4],
                            nombreCarrera = dr[5].ToString(),
                            tipo = dr[6].ToString(),
                            fecha = (DateTime)dr[7],
                            kilometraje = dr[8].ToString(),
                            altura = dr[9].ToString(),
                            duracion = dr[10].ToString(),
                            completitud = (bool)dr[11],
                            recorrido = dr[12].ToString()
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

        [Route("CarreraID")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Object carreraID(Carrera carrera)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("obteneridcarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombrecarr", NpgsqlTypes.NpgsqlDbType.Text, carrera.Nombrecarrera);
            int resp = (int)command.ExecuteScalar();
            var jsons = new[]
                    {
                        new {validacion = resp }
            };

            return jsons[0];
        }

        [Route("posicionesCarrera")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> posicionesCarrera([FromBody] Carrera carrera)
        {
            List<Object> CarrerasUser = new List<Object>();
            Usuarioscarrera usuarioCarrera = new Usuarioscarrera();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("posicionesCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idcarrera);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            nombre = dr[0].ToString(),
                            apellido = dr[1].ToString(),
                            edad = dr[2].ToString(),
                            categoria = dr[3].ToString(),
                            tiempo = dr[4].ToString()
                        }

                     };
                    Console.WriteLine(jsons);
                    CarrerasUser.Add(jsons);


                }

            }
            catch
            {
                Debug.WriteLine("Carrera no encontrada");

            }
            conn.Close();
            List<object> retornar = new List<object>();
            for (var x = 0; x < CarrerasUser.Count; x++)
            {
                var tempList = (IList<object>)CarrerasUser[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }
        [Route("deleteUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarUsuarioCarrera([FromBody] Usuarioscarrera user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarUsuarioCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iddep", NpgsqlTypes.NpgsqlDbType.Integer, user.Iddeportista);
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, user.Idcarrera);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("updateUserCarrera")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void actualizarDatosCarreraUsuario(Usuarioscarrera reto)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("actualizarDatosCarreraUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, reto.Iddeportista);
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, reto.Idcarrera);
            command.Parameters.AddWithValue("@distancia", NpgsqlTypes.NpgsqlDbType.Text, reto.Kilometraje);
            command.Parameters.AddWithValue("@alt", NpgsqlTypes.NpgsqlDbType.Text, reto.Altura);
            command.Parameters.AddWithValue("@tiempo", NpgsqlTypes.NpgsqlDbType.Text, reto.Tiemporegistrado);
            command.Parameters.AddWithValue("@completado", NpgsqlTypes.NpgsqlDbType.Boolean, reto.Completitud);
            command.Parameters.AddWithValue("@mapa", NpgsqlTypes.NpgsqlDbType.Text, reto.Recorrido);
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("buscarCategoria")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> usuariosGrupo([FromBody] Carrera carrera)
        {
            List<Object> User = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarCategoriaCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idcarrera);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            categoriasCarrera = dr[0].ToString(),
                        }

                     };
                    Console.WriteLine(jsons);
                    User.Add(jsons);


                }

            }
            catch
            {
                Debug.WriteLine("Grupo no encontrado");

            }
            conn.Close();
            List<object> retornar = new List<object>();
            for (var x = 0; x < User.Count; x++)
            {
                var tempList = (IList<object>)User[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }
    }
}
