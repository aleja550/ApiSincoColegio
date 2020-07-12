using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SincoABR.Models;

namespace SincoABR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : Controller
    {
        private readonly AppDbContext context;

        public EstudianteController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("ObtenerEstudiantes")]
        public IEnumerable<Estudiante> ObtenerEstudiantes()
        {
            return context.Estudiante.ToList();
        }

        [HttpGet]
        [Route("ObtenerUnEstudiante/{id}")]
        public Estudiante ObtenerUnEstudiante(int id)
        {
            Estudiante estudiante = context.Estudiante.FirstOrDefault(r => r.IdEstudiante == id);
            return estudiante;
        }

        [HttpPost]
        [Route("CrearEstudiante")]
        public ActionResult CrearEstudiante([FromBody]Estudiante estudiante)
        {
            try
            {
                context.Estudiante.Add(estudiante);
                context.SaveChanges();
                return Ok($"{estudiante.Cedula}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("EditarEstudiante/{id}")]
        public ActionResult EditarEstudiante(int id, [FromBody]Estudiante estudiante)
        {
            try
            {
                if (estudiante.IdEstudiante == id)
                {
                    context.Entry(estudiante).Property(e => e.Cedula).IsModified = true;
                    context.Entry(estudiante).Property(e => e.Nombres).IsModified = true;
                    context.Entry(estudiante).Property(e => e.Apellidos).IsModified = true;
                    context.SaveChanges();
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error ad was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpDelete]
        [Route("EliminarEstudiante/{id}")]
        public ActionResult EliminarEstudiante(int id)
        {
            Estudiante student = context.Estudiante.FirstOrDefault(r => r.IdEstudiante == id);
            try
            {
                if (student != null)
                {
                    context.Estudiante.Remove(student);
                    context.SaveChanges();
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error ad was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpGet]
        [Route("ObtenerReporte/{username}")]
        public List<Reporte> ObtenerReporte(string username)
        {
            List<Reporte> reporteNotas = null;
            string query = @"Select E.IdEstudiante,E.Cedula, E.Nombres, E.Apellidos,N.Nota1, N.Nota2, M.NombreMateria[Materia], P.Nombres[Profesor]
                            FROM Notas N 
                                JOIN Materia M ON N.FKIdMateria = M.IdMateria 
                                JOIN Estudiante E ON N.FKIdEstudiante = E.IdEstudiante
                                JOIN Profesor P ON M.IdMateria = P.FkIdMateria 
                            WHERE E.IdEstudiante = (SELECT IdEstudiante FROM Estudiante E
						                                   JOIN Usuario U ON E.FKUsuario = U.IdUsuario
						                            WHERE U.Username = @username)";
            try
            {
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@username", username));
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader?.HasRows ?? false)
                        {
                            reporteNotas = new List<Reporte>();
                            while (reader.Read())
                            {
                                var reporte = new Reporte()
                                {
                                    IdEstudiante  = Convert.ToInt32(reader["IdEstudiante"]),
                                    Cedula = Convert.ToInt64(reader["Cedula"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Nota1 = reader["Nota1"].ToString(),
                                    Nota2 = reader["Nota2"].ToString(),
                                    Materia = reader["Materia"].ToString(),
                                    Profesor = reader["Profesor"].ToString()
                                };

                                reporteNotas.Add(reporte);
                            }
                        }
                    }
                }

                return reporteNotas;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
