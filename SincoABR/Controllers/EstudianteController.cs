using System;
using System.Collections.Generic;
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
                return Ok($"{estudiante.Cedula}.");
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
            Estudiante student = context.Estudiante.FirstOrDefault(r => r.IdEstudiante == id);
            try
            {
                if (student != null)
                {
                    context.Entry(estudiante).State = EntityState.Modified;
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
    }
}
