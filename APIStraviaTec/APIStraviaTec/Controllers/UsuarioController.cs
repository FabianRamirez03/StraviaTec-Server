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
    
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private string serverKey = Startup.getKey();
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM usuario", conn);

            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();


            string valor = "";
            // Output rows
            while (dr.Read())
            {
                Console.Write("{0}\t{1} \n", dr[0], dr[1]);
                valor += (string)dr[1];
                
            }
           

            conn.Close();

            return new string[] {valor, "value2" };
        }

        [Route("porUsuarioId")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Usuario PostUsuarioId([FromBody] Usuario usuario)
        {
            Usuario Usuarioret = new Usuario();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarUsuarioId", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    Usuarioret.Idusuario = (int)dr[0];
                    Usuarioret.Nombreusuario = dr[1].ToString();
                    Usuarioret.Contrasena = dr[2].ToString();
                    Usuarioret.Primernombre = dr[3].ToString();
                    Usuarioret.Apellidos = dr[4].ToString();
                    Usuarioret.Fechanacimiento = (DateTime)dr[5];
                    Usuarioret.Nacionalidad = dr[6].ToString();
                    if (dr[7] == DBNull.Value)
                    {
                        Usuarioret.Foto = null;
                    }
                    else
                    {
                        Usuarioret.Foto = (string)dr[7];
                    }
                    Usuarioret.Carrera = null;
                    Usuarioret.Reto = null;
                    Usuarioret.Grupo = null;
                    string json = JsonConvert.SerializeObject(Usuarioret);


                }

            }
            catch
            {
                Debug.WriteLine("Producto no encontrado");

            }
            conn.Close();
            return Usuarioret;
        }

        [Route("crearUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Usuario PostcrearUsuario([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Nombreusuario);
            command.Parameters.AddWithValue("@contra", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Contrasena);
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Primernombre);
            command.Parameters.AddWithValue("@apellido", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Apellidos);
            command.Parameters.AddWithValue("@nacimiento", NpgsqlTypes.NpgsqlDbType.Date, usuario.Fechanacimiento);
            command.Parameters.AddWithValue("@pais", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Nacionalidad);
            command.Parameters.AddWithValue("@imagen", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Foto);
            command.Parameters.AddWithValue("@administra", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Administra);

            // Execute the query and obtain a result set
            command.ExecuteNonQuery();
            Debug.WriteLine("Usuario creado exitosamente");
            conn.Close();
            return usuario;
        }

        [Route("existeUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Object existUsuario(Usuario usuario)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("validacionDeUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombuser", NpgsqlTypes.NpgsqlDbType.Text, usuario.Nombreusuario);
            bool resp = (Boolean)command.ExecuteScalar();
            var jsons = new[]
                    {
                        new {validacion = resp }
            };

            return jsons[0];
        }

        [Route("buscarUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> PostbuscaUsuario([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscaUsuarioSimilar", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombbusc", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Primernombre);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    Usuario interno = new Usuario();

                    interno.Idusuario = (int)dr[0];

                    interno.Nombreusuario = dr[1].ToString();

                    interno.Primernombre = dr[2].ToString();

                    interno.Apellidos = dr[3].ToString();

                    interno.Foto = dr[4].ToString();

                    string json = JsonConvert.SerializeObject(interno);

                    Usuarioret.Add(interno);


                }

            }
            catch
            {
                Debug.WriteLine("Producto no encontrado");

            }
            conn.Close();
            return Usuarioret;
        }

        [Route("modificarUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> PostmodificarUsuario([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("modificarUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            command.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Nombreusuario);
            command.Parameters.AddWithValue("@contra", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Contrasena);
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Primernombre);
            command.Parameters.AddWithValue("@apellido", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Apellidos);
            command.Parameters.AddWithValue("@nacimiento", NpgsqlTypes.NpgsqlDbType.Date, usuario.Fechanacimiento);
            command.Parameters.AddWithValue("@pais", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Nacionalidad);
            command.Parameters.AddWithValue("@imagen", NpgsqlTypes.NpgsqlDbType.Bytea, usuario.Foto);
            // Execute the query and obtain a result set
            command.ExecuteNonQuery();
            Debug.WriteLine("Usuario creado exitosamente");
            conn.Close();
            return Usuarioret;
        }

        [Route("getSeguidores")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public object getSeguidores([FromBody] Usuario usuario)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("cantSeguidores", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idusuario", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            // Execute the query and obtain a result set
            Int64 seguidores = (Int64)command.ExecuteScalar();
            NpgsqlCommand command2 = new NpgsqlCommand("cantSiguiendo", conn);
            command2.CommandType = System.Data.CommandType.StoredProcedure;
            command2.Parameters.AddWithValue("@idusuario", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            Int64 seguidos = (Int64)command2.ExecuteScalar();

            var jsons = new[]
                    {
                        new {
                            Seguidores= seguidores,
                            Seguidos = seguidos,
                        }

                     };

            conn.Close();
            return jsons;
        }

        [Route("eliminarUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> PosteliminarUsuario([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarUsuarioID", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            // Execute the query and obtain a result set
            command.ExecuteNonQuery();
            Debug.WriteLine("Usuario eliminado exitosamente");
            conn.Close();
            return Usuarioret;
        }

        [Route("friendsActivity")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        
        public List<object> actividadesAmigos(Usuario usuario)
        {
            List<Object> Actividad = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("verActividadesAmigos", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idusuario", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idusuario);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    var jsons = new[]
                    {
                        new {
                            idAmigo= (int)dr[0],
                            nombreUsuario = dr[1].ToString(),
                            nombreAmigo = dr[2].ToString(),
                            apellidoAmigo = dr[3].ToString(),
                            fotoAmigo = dr[4].ToString(),
                            nombreactividad = dr[5].ToString(),
                            tipo = dr[6].ToString(),
                            fecha = (DateTime) dr[7],
                            mapa = dr[8].ToString(),
                            kilometros = dr[9].ToString()
                        }

                     };
                    Console.WriteLine(jsons);
                    Actividad.Add(jsons);

                }

            }
            catch
            {
                Debug.WriteLine("Carrera no encontrada");

            }
            conn.Close();
            List<object> retornar = new List<object>();
            for (var x = 0; x < Actividad.Count; x++)
            {
                var tempList = (IList<object>)Actividad[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }

        [Route("friendsCarreras")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]

        public List<object> carrerasAmigos(Usuario usuario)
        {
            List<Object> Actividad = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("carrerasAmigos", conn);
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
                            nombreAmigo = dr[0].ToString(),
                            actividad = dr[1].ToString(),
                            tipo = dr[2].ToString(),
                            fecha = (DateTime) dr[3],
                            kilometros = dr[4].ToString(),
                            altura = dr[5].ToString(),
                            duracion = dr[6].ToString(),
                            mapa = dr[7].ToString(),
                            
                        }

                     };
                    Console.WriteLine(jsons);
                    Actividad.Add(jsons);

                }

            }
            catch
            {
                Debug.WriteLine("Carrera no encontrada");

            }
            conn.Close();
            List<object> retornar = new List<object>();
            for (var x = 0; x < Actividad.Count; x++)
            {
                var tempList = (IList<object>)Actividad[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }

        [Route("porNombreUsuario")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Usuario buscarUsuariousername([FromBody] Usuario usuario)
        {
            Usuario Usuarioret = new Usuario();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarUsuariousername", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Nombreusuario);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    Usuarioret.Idusuario = (int)dr[0];
                    Usuarioret.Nombreusuario = dr[1].ToString();
                    Usuarioret.Contrasena = dr[2].ToString();
                    Usuarioret.Primernombre = dr[3].ToString();
                    Usuarioret.Apellidos = dr[4].ToString();
                    Usuarioret.Fechanacimiento = (DateTime)dr[5];
                    Usuarioret.Nacionalidad = dr[6].ToString();
                    Usuarioret.Foto = (string)dr[7];
                    Usuarioret.Edad = (int)dr[8];
                    Usuarioret.Categoria = dr[9].ToString();
                    Usuarioret.Administra = (int)dr[10];
                    Usuarioret.Carrera = null;
                    Usuarioret.Reto = null;
                    Usuarioret.Grupo = null;
                    string json = JsonConvert.SerializeObject(Usuarioret);


                }

            }
            catch
            {
                Debug.WriteLine("Usuario no encontrado");

            }
            conn.Close();
            return Usuarioret;
        }

        [Route("Groups")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> gruposUsuario([FromBody] Usuario usuario)
        {
            List<Object> User = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarGruposUsuario", conn);
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
                            nombreGrupo = dr[0].ToString(),
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

        [Route("friendsRetos")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]

        public List<object> retosAmigos(Usuario usuario)
        {
            List<Object> Actividad = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("retosAmigos", conn);
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
                            nombreAmigo = dr[0].ToString(),
                            reto = dr[1].ToString(),
                            tipoReto = dr[2].ToString(),
                            tipoActividad = dr[3].ToString(),
                            fecha = (DateTime) dr[4],
                            kilometros = dr[5].ToString(),
                            altura = dr[6].ToString(),
                            duracion = dr[7].ToString(),
                            mapa = dr[8].ToString(),

                        }

                     };
                    Console.WriteLine(jsons);
                    Actividad.Add(jsons);

                }

            }
            catch
            {
                Debug.WriteLine("Carrera no encontrada");

            }
            conn.Close();
            List<object> retornar = new List<object>();
            for (var x = 0; x < Actividad.Count; x++)
            {
                var tempList = (IList<object>)Actividad[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }

        [Route("validarUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public Object validarUsuario(Usuario usuario)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("tipoCuentaUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Text, usuario.Nombreusuario);
            command.Parameters.AddWithValue("@clave", NpgsqlTypes.NpgsqlDbType.Text, usuario.Contrasena);
            int resp = (int) command.ExecuteScalar();
            var jsons = new[]
                    {
                        new {validacion = resp }
            };

            return jsons[0];
        }
        [Route("agregarAmigo")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> PostagregarAmigo([FromBody] Amigosusuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarAmigo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Iddeportista);
            command.Parameters.AddWithValue("@idamigo", NpgsqlTypes.NpgsqlDbType.Integer, usuario.Idamigo);
            // Execute the query and obtain a result set
            command.ExecuteNonQuery();
            Debug.WriteLine("Amigo agragado exitosamente");
            conn.Close();
            return Usuarioret;
        }
    }  
}
