using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SincoABR.Models
{
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }
        public long Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int TipoUser { get; set; }
    }
}
