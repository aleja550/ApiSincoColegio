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
    public class NotasController : Controller
    {
        private readonly AppDbContext context;

        public NotasController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("ObtenerNotas")]
        public IEnumerable<Notas> ObtenerNotas()
        {
            return context.Notas.ToList();
        }

        [HttpGet]
        [Route("ObtenerUnaNota/{id}")]
        public Notas ObtenerUnaNota(int id)
        {
            Notas notas = context.Notas.FirstOrDefault(r => r.IdNotas == id);
            return notas;
        }

        [HttpPost]
        [Route("CrearNotas")]
        public ActionResult CrearNotas([FromBody]Notas notas)
        {
            try
            {
                context.Notas.Add(notas);
                context.SaveChanges();
                return Ok($"{notas.IdNotas}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("EditarNotas/{id}")]
        public ActionResult EditarNotas(int id, [FromBody]Notas notas)
        {
            try
            {
                if (notas.IdNotas == id)
                {
                    context.Entry(notas).Property(e => e.Nota1).IsModified = true;
                    context.Entry(notas).Property(e => e.Nota2).IsModified = true;
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
        [Route("EliminarNotas/{id}")]
        public ActionResult EliminarNotas(int id)
        {
            Notas notas = context.Notas.FirstOrDefault(r => r.IdNotas == id);
            try
            {
                if (notas != null)
                {
                    context.Notas.Remove(notas);
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
        [Route("ObtenerNotasPorMateria/{idmateria}")]
        public List<Calificacion> ObtenerNotasPorMateria(int idmateria)
        {
            List<Calificacion> dataCalificaciones = null;
            string query = @"Select IdNotas, E.Nombres, E.Apellidos,N.Nota1, N.Nota2 from Notas N 
                             JOIN Materia M ON N.FKIdMateria = M.IdMateria 
                             JOIN Estudiante E ON N.FKIdEstudiante = E.IdEstudiante
                             WHERE M.IdMateria = @IdMateria";
            try
            {
                using (var cmd = context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@IdMateria", idmateria));
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader?.HasRows ?? false)
                        {
                            dataCalificaciones = new List<Calificacion>();
                            while (reader.Read())
                            {
                                var notas = new Calificacion()
                                {
                                    IdNotas = Convert.ToInt32(reader["IdNotas"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Nota1 = reader["Nota1"].ToString(),
                                    Nota2 = reader["Nota2"].ToString()
                                };

                                dataCalificaciones.Add(notas);
                            }
                        }
                    }
                }

                    return dataCalificaciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[HttpGet]
        //[Route("ObtenerNotasPorMateria/{idmateria}")]
        //public List<string> ObtenerNotasPorMateria(int idmateria)
        //{
        //    var gettinValues = context.Notas.Join(context.Materia, n => n.FKIdMateria, m => m.IdMateria,
        //                     (n, m) => new { Notas = n, Materia = m })
        //                     .Join(context.Estudiante, n => n.Notas.FKIdEstudiante, e => e.IdEstudiante,
        //                          (n, e) => new { Notas = n, Estudiante = e }).Where(ne => ne.Notas.Notas.FKIdMateria == idmateria);
        //    try
        //    {
        //        List<Notas> notesList = gettinValues.Select(g => g.Notas.Notas).ToList();
        //        List<Estudiante> studentList = gettinValues.Select(e => e.Estudiante).ToList();

        //        var output = notesList.Join(studentList, a => a.FKIdEstudiante, b => b.IdEstudiante, (a, b) =>
        //        ("{"+$"IdNotas: {a.IdNotas}, Nombres: {b.Nombres}, Apellidos: {b.Apellidos}, Nota1: {a.Nota1}, Nota2: {a.Nota2}" +"}")).ToList();

        //        var test = (from o in output
        //                    select o.ToString()).ToList();

        //        return test;
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}