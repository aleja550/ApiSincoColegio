using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SincoABR.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Contraseña { get; set; }
        public int TipoUser { get; set; }
    }
}
