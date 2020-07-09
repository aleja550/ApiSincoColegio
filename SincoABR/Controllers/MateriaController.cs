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
    public class MateriaController : Controller
    {
        private readonly AppDbContext context;

        public MateriaController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("ObtenerMaterias")]
        public IEnumerable<Materia> ObtenerMaterias()
        {
            return context.Materia.ToList();
        }

        [HttpGet]
        [Route("ObtenerUnaMateria/{id}")]
        public Materia ObtenerUnaMateria(int id)
        {
            Materia materia = context.Materia.FirstOrDefault(r => r.IdMateria == id);
            return materia;
        }

        [HttpPost]
        [Route("CrearMateria")]
        public ActionResult CrearMateria([FromBody]Materia materia)
        {
            try
            {
                context.Materia.Add(materia);
                context.SaveChanges();
                return Ok($"{materia.IdMateria}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("EditarMateria/{id}")]
        public ActionResult EditarMateria(int id, [FromBody]Materia materia)
        {
            Materia assignature = context.Materia.FirstOrDefault(r => r.IdMateria == id);
            try
            {
                if (assignature != null)
                {
                    context.Entry(materia).State = EntityState.Modified;
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
        [Route("EliminarMateria/{id}")]
        public ActionResult EliminarMateria(int id)
        {
            Materia materia = context.Materia.FirstOrDefault(r => r.IdMateria == id);
            try
            {
                if (materia != null)
                {
                    context.Materia.Remove(materia);
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