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
    [Route("[controller]")]
    [ApiController]
    public class UsuariosCarreraController : ControllerBase
    {
        private string serverKey = Startup.getKey();
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
            conn.Close();
            return CarrerasUser;
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

    }
}
