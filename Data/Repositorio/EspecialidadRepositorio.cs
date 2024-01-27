using Data.Interfaces.IRepositorio;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositorio
{
    public class EspecialidadRepositorio : Repositorio<Especialidad>, IEspecialidadRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EspecialidadRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actializar(Especialidad especialidad)
        {
            var especialidadDb = _db.Especialidades.FirstOrDefault(e => e.Id == especialidad.Id);
            if (especialidadDb != null)
            {
                especialidad.NombreEspecialidad = especialidad.NombreEspecialidad;
                especialidad.Descripcion = especialidad.Descripcion;
                especialidad.Estado = especialidad.Estado;
                especialidad.FechaActualizacion = DateTime.Now;
                _db.SaveChanges();
            }
        }
    }
}
