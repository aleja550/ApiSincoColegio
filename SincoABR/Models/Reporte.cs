using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SincoABR.Models
{
    public class Reporte
    {
        public int IdEstudiante { get; set; }
        public long Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Nota1 { get; set; }
        public string Nota2 { get; set; }
        public string Materia { get; set; }
        public string Profesor { get; set; }
    }
}
