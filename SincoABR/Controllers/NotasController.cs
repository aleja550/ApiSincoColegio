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
                return Ok($"{notas.IdNotas}.");
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
            Notas note = context.Notas.FirstOrDefault(r => r.IdNotas == id);
            try
            {
                if (note != null)
                {
                    context.Entry(notas).State = EntityState.Modified;
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
    }
}