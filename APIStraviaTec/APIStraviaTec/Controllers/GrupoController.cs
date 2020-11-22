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
            Debug.WriteLine("Eliminado exitosamente");
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
            Debug.WriteLine("Modificado exitosamente");            
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

        [Route("Groups")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> buscarGrupos([FromBody] Grupo grupo)
        {
            List<Object> grupos = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarGrupos", conn);
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
                            nombreGrupo = dr[0].ToString(),
                            nombreAdmin = dr[1].ToString(),
                            apellidoAdmin = dr[2].ToString(),
                        }

                     };
                    Console.WriteLine(jsons);
                    grupos.Add(jsons);


                }

            }
            catch
            {
                Debug.WriteLine("Grupo no encontrado");

            }
            conn.Close();
            return grupos;
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

        [Route("addCarreras")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarCarreras([FromBody] Carrerasgrupo user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarCarreraGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, user.Idgrupo);
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Integer, user.Idcarrera);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("addRetos")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarRetos([FromBody] Retosgrupo user)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("agregarRetoGrupo", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, user.Idgrupo);
            command.Parameters.AddWithValue("@idret", NpgsqlTypes.NpgsqlDbType.Integer, user.Idreto);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

        [Route("carrerasInGroup")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> buscarCarrera([FromBody] Grupo grupo)
        {
            List<Object> User = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarCarrerasGrupo", conn);
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
                            idCarrera = (int)dr[0],
                            idOrg = (int)dr[1],
                            nombCar = dr[2].ToString(),
                            fecha = (DateTime) dr[3],
                            categoria = dr[3].ToString(),
                            tipo = dr[4].ToString(),
                            costo = (int)dr[5],
                            cuenta = dr[6].ToString(),
                            mapa = dr[7].ToString(),
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

        [Route("retosInGroup")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public List<Object> buscarRetos([FromBody] Grupo grupo)
        {
            List<Object> User = new List<Object>();
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("buscarRetosGrupo", conn);
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
                            idReto = (int)dr[0],
                            idOrg = (int)dr[1],
                            nombRet = dr[2].ToString(),
                            tipoAct = dr[3].ToString(),
                            tipoRet = dr[4].ToString(),
                            fechaIn = (DateTime) dr[5],
                            fechaFin = (DateTime) dr[6],
                            objetivo = dr[7].ToString(),
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
