using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class MedicoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe ser Minimo 1 Maximo 60 caracteres")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser Minimo 1 Maximo 60 caracteres")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Direccion es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser Minimo 1 Maximo 100 caracteres")]
        public string Direccion { get; set; }
        [MaxLength(40)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Genero es requerido")]
        public char Genero { get; set; }
        [Required(ErrorMessage = "Especialidad es Requerida")]
        public int EspecialidadId { get; set; }
        public string NombreEspecialidad { get; set; }
        public int Estado { get; set; }
    }
}
