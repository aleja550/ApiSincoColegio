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
    public class UsuarioController : Controller
    {
        private readonly AppDbContext context;

        public UsuarioController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return context.Usuario.ToList();
        }

        [HttpGet]
        [Route("ObtenerUnUsuario/{username}")]
        public Usuario ObtenerUnUsuario(string username)
        {
            Usuario usuario = context.Usuario.FirstOrDefault(r => r.Username == username);
            return usuario;
        }

        [HttpGet]
        [Route("ValidateLogin/{username},{password}")]
        public Usuario ObtenerUsuarioPorNombre(string username, string password)
        {
            Usuario usuario = context.Usuario.FirstOrDefault(r => r.Username == username && r.Contraseña == password);
            return usuario;
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public ActionResult CrearUsuario([FromBody]Usuario usuario)
        {
            try
            {
                context.Usuario.Add(usuario);
                context.SaveChanges();
                return Ok($"{usuario.IdUsuario}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"The server encountered an internal error was unable to complete your request. {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut]
        [Route("EditarUsuario/{id}")]
        public ActionResult EditarUsuario(int id, [FromBody]Usuario usuario)
        {
            Usuario user = context.Usuario.FirstOrDefault(r => r.IdUsuario == id);
            try
            {
                if (user != null)
                {
                    context.Entry(usuario).State = EntityState.Modified;
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
        [Route("EliminarUsuario/{id}")]
        public ActionResult EliminarMateria(int id)
        {
            Usuario usuario = context.Usuario.FirstOrDefault(r => r.IdUsuario == id);
            try
            {
                if (usuario != null)
                {
                    context.Usuario.Remove(usuario);
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