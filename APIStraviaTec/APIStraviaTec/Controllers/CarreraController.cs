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
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private string serverKey = Startup.getKey();
        [Route("createActivity")]
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
            command.Parameters.AddWithValue("@carreracategoria", NpgsqlTypes.NpgsqlDbType.Date, carrera.Categoria);
            command.Parameters.AddWithValue("@recorridocarrera", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Recorrido);
            command.Parameters.AddWithValue("@privacidad", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Privada);
            command.Parameters.AddWithValue("@precio", NpgsqlTypes.NpgsqlDbType.Date, carrera.Costo);
            command.Parameters.AddWithValue("@cuentabanc", NpgsqlTypes.NpgsqlDbType.Varchar, carrera.Cuentabancaria);
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
            NpgsqlCommand command = new NpgsqlCommand("eliminarActividad", conn);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@idact", NpgsqlTypes.NpgsqlDbType.Integer, carrera.Idactividad);
            // Execute the query and obtain a result set
            NpgsqlDataReader dr = command.ExecuteReader();
            conn.Close();
            return;
        }

    }
}
