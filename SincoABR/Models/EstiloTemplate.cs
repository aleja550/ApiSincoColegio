using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SincoABR.Models
{
    public class EstiloTemplate
    {
        [Key]
        public byte CodigoTemplate { get; set; }
        public byte[] ImagenTemplate { get; set; }
        public string TituloTemplate { get; set; }
    }
}

