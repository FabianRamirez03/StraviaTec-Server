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
        public List<Usuario> PostUsuarioId([FromBody] Usuario usuario)
        {
            List<Usuario> Usuarioret = new List<Usuario>();
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
            NpgsqlCommand command = new NpgsqlCommand("eagregarAmigo", conn);
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
