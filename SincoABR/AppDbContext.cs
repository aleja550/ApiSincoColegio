using Microsoft.EntityFrameworkCore;
using SincoABR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SincoABR
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<EstiloTemplate> EstiloTemplate { get; set; }
    }
}
