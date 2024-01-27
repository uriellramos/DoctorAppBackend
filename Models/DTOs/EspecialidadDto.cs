using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EspecialidadDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El Nombre debe ser Minimo 1 Maximo 60 Caracteres")]
        public string NombreEspecialidad { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El Descripcion debe ser Minimo 1 Maximo 100 Caracteres")]
        public string Descripcion { get; set; }

        public int Estado { get; set; }
    }
}
