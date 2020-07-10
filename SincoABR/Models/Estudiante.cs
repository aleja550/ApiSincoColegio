using System.ComponentModel.DataAnnotations;


namespace SincoABR.Models
{
    public class Estudiante
    {
        [Key]
        public int IdEstudiante { get; set; }
        public long Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int FKUsuario { get; set; }
    }
}
