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
    public class ProfesorController : Controller
    {
        private readonly AppDbContext context;

        public ProfesorController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("ObtenerProfesores")]
        public IEnumerable<Profesor> ObtenerProfesores()
        {
            return context.Profesor.ToList();
        }

        [HttpGet]
        [Route("ObtenerUnProfesor/{id}")]
        public Profesor ObtenerUnProfesor(int id)
        {
            Profesor profesor = context.Profesor.FirstOrDefault(r => r.IdProfesor == id);
            return profesor;
        }

        [HttpGet]
        [Route("ObtenerProfesorCedula/{id}")]
        public Profesor ObtenerProfesorCedula(long id)
        {
            Profesor profesor = context.Profesor.FirstOrDefault(r => r.Cedula == id);
            return profesor;
        }

        [HttpPost]
        [Route("CrearProfesor")]
        public ActionResult CrearProfesor([FromBody]Profesor profesor)
        {
            try
            {
                context.Profesor.Add(profesor);
                context.SaveChanges();
                return Ok($"{profesor.Cedula}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("EditarProfesor/{id}")]
        public ActionResult EditarProfesor(int id, [FromBody]Profesor profesor)
        {
            Profesor teacher = context.Profesor.FirstOrDefault(r => r.IdProfesor == id);
            try
            {
                if (teacher != null)
                {
                    context.Entry(profesor).State = EntityState.Modified;
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
        [Route("EliminarProfesor/{id}")]
        public ActionResult EliminarEstudiante(int id)
        {
            Profesor profesor = context.Profesor.FirstOrDefault(r => r.IdProfesor == id);
            try
            {
                if (profesor != null)
                {
                    context.Profesor.Remove(profesor);
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