using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SincoABR.Models
{
    public class Notas
    {
        [Key]
        public int IdNotas { get; set; }
        public string Nota1 { get; set; }
        public string Nota2 { get; set; }
        public int FKIdMateria { get; set; }
        public int FKIdEstudiante { get; set; }
    }
}
