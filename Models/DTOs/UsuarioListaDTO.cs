using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UsuarioListaDTO
    {
        public string Username { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}
