using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SincoABR.Models;

namespace SincoABR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstiloTemplateController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstiloTemplateController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("ObtenerTemplates")]
        public IEnumerable<EstiloTemplate> ObtenerMaterias()
        {
            return context.EstiloTemplate.ToList();
        }
    }
}