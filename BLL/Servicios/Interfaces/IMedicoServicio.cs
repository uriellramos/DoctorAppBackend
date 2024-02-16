using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IMedicoServicio
    {
        Task<IEnumerable<MedicoDTO>> ObtenerTodos();
        Task<MedicoDTO> Agregar(MedicoDTO modeloDto);
        Task Actualizar(MedicoDTO modeloDto);
        Task Remover(int id);
    }
}
