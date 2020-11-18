﻿using System;
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
                        Usuarioret.Foto = (byte[])dr[7];
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
        public List<Usuario> PostcrearUsuario([FromBody] Usuario usuario)
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
            // Execute the query and obtain a result set
            command.ExecuteNonQuery();
            Debug.WriteLine("Usuario creado exitosamente");
            conn.Close();
            return Usuarioret;
        }

        [Route("porUsuarioName")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> PostUsuarioName([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarUsuarioNombre", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Primernombre);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    usuario.Idusuario = (int)dr[0];
                    usuario.Nombreusuario = dr[1].ToString();
                    usuario.Contrasena = dr[2].ToString();
                    usuario.Primernombre = dr[3].ToString();
                    usuario.Apellidos = dr[4].ToString();
                    usuario.Fechanacimiento = (DateTime)dr[5];
                    usuario.Nacionalidad = dr[6].ToString();
                    if (dr[7] == DBNull.Value)
                    {
                        usuario.Foto = null;
                    }
                    else
                    {
                        usuario.Foto = (byte[])dr[7];
                    }
                    usuario.Carrera = null;
                    usuario.Reto = null;
                    usuario.Grupo = null;
                    string json = JsonConvert.SerializeObject(usuario);
                    Usuarioret.Add(usuario);


                }

            }
            catch
            {
                Debug.WriteLine("Producto no encontrado");

            }
            conn.Close();
            return Usuarioret;
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
                    if (dr[7] == DBNull.Value)
                    {
                        Usuarioret.Foto = null;
                    }
                    else
                    {
                        Usuarioret.Foto = (byte[])dr[7];
                    }
                    if (dr[8] == DBNull.Value)
                    {
                        Usuarioret.Edad = null;
                    }
                    else
                    {
                        Usuarioret.Edad = (int)dr[8];
                    }
                    if(dr[9] == DBNull.Value)
                    {
                        Usuarioret.Categoria = null;
                    }
                    else
                    {
                        Usuarioret.Categoria = dr[9].ToString();
                    }
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
        [Route("porUsuarioNP")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Usuario> PostUsuarioNP([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarUsuarioNombreApellido", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Primernombre);
            command.Parameters.AddWithValue("@apellido", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Apellidos);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    usuario.Idusuario = (int)dr[0];
                    usuario.Nombreusuario = dr[1].ToString();
                    usuario.Contrasena = dr[2].ToString();
                    usuario.Primernombre = dr[3].ToString();
                    usuario.Apellidos = dr[4].ToString();
                    usuario.Fechanacimiento = (DateTime)dr[5];
                    usuario.Nacionalidad = dr[6].ToString();
                    if (dr[7] == DBNull.Value)
                    {
                        usuario.Foto = null;
                    }
                    else
                    {
                        usuario.Foto = (byte[])dr[7];
                    }
                    usuario.Carrera = null;
                    usuario.Reto = null;
                    usuario.Grupo = null;
                    string json = JsonConvert.SerializeObject(usuario);
                    Usuarioret.Add(usuario);
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
            NpgsqlCommand command = new NpgsqlCommand("eliminarUsuarioNombre", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Varchar, usuario.Nombreusuario);
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
            NpgsqlCommand command = new NpgsqlCommand("TodasActividadesAmigos", conn);
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
                            nombreAmigo = dr[0].ToString(),
                            actividad = dr[1].ToString(),
                            tipo = dr[2].ToString(),
                            fecha = (DateTime) dr[3],
                            mapa = dr[4].ToString(),
                            kilometros = dr[5].ToString()
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
            NpgsqlCommand command = new NpgsqlCommand("validacionDeUsuario", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", NpgsqlTypes.NpgsqlDbType.Text, usuario.Nombreusuario);
            command.Parameters.AddWithValue("@clave", NpgsqlTypes.NpgsqlDbType.Text, usuario.Contrasena);
            bool resp = (Boolean) command.ExecuteScalar();
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
