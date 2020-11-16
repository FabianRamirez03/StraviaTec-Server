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
    public class GrupoController : ControllerBase
    {
        private string serverKey = Startup.getKey();
        [Route("deleteUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarUsuario([FromBody] Usuariosporgrupo user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarUsuarioGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, user.Idusuario);
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, user.Idgrupo);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("addUser")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarUsuario([FromBody] Usuariosporgrupo user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarUsuarioGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Integer, user.Idusuario);
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, user.Idgrupo);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("deleteGroup")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarGrupo([FromBody] Grupo grupo)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("EliminarGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, grupo.Idgrupo);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("modifyGroup")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void modificarGrupo([FromBody] Grupo grupo)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("modificarGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, grupo.Idgrupo);
            command.Parameters.AddWithValue("@nombgrup", NpgsqlTypes.NpgsqlDbType.Varchar, grupo.Nombre);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("addGroup")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarGrupo([FromBody] Grupo grupo)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombgrup", NpgsqlTypes.NpgsqlDbType.Varchar, grupo.Nombre);
            command.Parameters.AddWithValue("@idadmin", NpgsqlTypes.NpgsqlDbType.Integer, grupo.Idadministrador);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("usersInGroup")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> usuariosGrupo([FromBody] Grupo grupo)
        {
            List<Object> User = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("usuariosGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, grupo.Idgrupo);
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
                            idUsuario = (int)dr[1],
                            nombre = dr[2].ToString(),
                            apellidos = dr[3].ToString(),
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
            for (var x = 0; x <  User.Count; x++)
            {
                var tempList = (IList<object>)User[x];
                retornar.Add(tempList[0]);
            }
            return retornar;
        }
    }
}
