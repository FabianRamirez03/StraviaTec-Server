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
            command.Parameters.AddWithValue("@idgroup", NpgsqlTypes.NpgsqlDbType.Integer, actividad.Idactividad);
            command.Parameters.AddWithValue("@nombgrup", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Nombreactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
        [Route("addActivity")]
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void agregarActividad([FromBody] Actividad actividad)
        {
            //Connect to a PostgreSQL database
            NpgsqlConnection conn = new NpgsqlConnection(serverKey);
            conn.Open();
            // Define a query returning a single row result set 
            NpgsqlCommand command = new NpgsqlCommand("crearActividad", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@nombre", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Nombreactividad);
            command.Parameters.AddWithValue("@fechaact", NpgsqlTypes.NpgsqlDbType.Date, actividad.Fecha);
            command.Parameters.AddWithValue("@idadmin", NpgsqlTypes.NpgsqlDbType.Varchar, actividad.Tipoactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }
    }
}
