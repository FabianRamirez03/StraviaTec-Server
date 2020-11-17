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
    public class AfiliacionesController : ControllerBase
    {
        private string serverKey = Startup.getKey();
        [Route("deleteAfiliacion")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void eliminarSolicitud(Solicitudescarrera usuario)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("eliminarSolicitud", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Text, usuario.Idusuario);
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Text, usuario.Idcarrera);
            command.ExecuteScalar();
        }
        [Route("acceptAfiliacion")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void aceptarSolicitud(Solicitudescarrera usuario)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("aceptarSolicitud", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Text, usuario.Idcarrera);
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Text, usuario.Idusuario);
            command.ExecuteScalar();
        }

        [Route("sendAfiliacion")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void enviarSolicitudCarrera(Solicitudescarrera usuario)
        {
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("enviarSolicitudCarrera", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idcarr", NpgsqlTypes.NpgsqlDbType.Text, usuario.Idcarrera);
            command.Parameters.AddWithValue("@iduser", NpgsqlTypes.NpgsqlDbType.Text, usuario.Idusuario);
            command.Parameters.AddWithValue("@recib", NpgsqlTypes.NpgsqlDbType.Bytea, usuario.Recibo);
            command.ExecuteScalar();
        }
    }


}

