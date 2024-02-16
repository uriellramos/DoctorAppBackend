using AutoMapper;
using BLL.Servicios.Interfaces;
using Data.Interfaces.IRepositorio;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class EspecialidadServicio : IEspecialidadServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public EspecialidadServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task<EspecialidadDto> Agregar(EspecialidadDto modeloDto)
        {
            try
            {
                Especialidad especialidad = new Especialidad
                {
                    NombreEspecialidad = modeloDto.NombreEspecialidad,
                    Descripcion = modeloDto.Descripcion,
                    Estado = modeloDto.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                };
                await _unidadTrabajo.Especialidad.Agrgar(especialidad);
                await _unidadTrabajo.Guardar();
                if (especialidad.Id == 0)
                    throw new TaskCanceledException("La especialidad no se pudo crear");
                return _mapper.Map<EspecialidadDto>(especialidad);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Actualizar(EspecialidadDto modeloDto)
        {
            try
            {
                var especialidadBd = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (especialidadBd == null)
                    throw new TaskCanceledException("La especialidad no existe");
                especialidadBd.NombreEspecialidad = modeloDto.NombreEspecialidad;
                especialidadBd.Descripcion = modeloDto.Descripcion;
                especialidadBd.Estado = modeloDto.Estado == 1 ? true : false;
                _unidadTrabajo.Especialidad.Actializar(especialidadBd);
                await _unidadTrabajo.Guardar();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var especialidadBd = await _unidadTrabajo.Especialidad.ObtenerPrimero(e => e.Id == id);
                if (especialidadBd == null)
                    throw new TaskCanceledException("La especialidad no existe");
                _unidadTrabajo.Especialidad.Remover(especialidadBd);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EspecialidadDto>> ObtenerTodos()
        {
            try
            {
                var lista  = await _unidadTrabajo.Especialidad.ObtenerTodos(
                    orderBy: e=> e.OrderBy(e  => e.NombreEspecialidad));
                return _mapper.Map<IEnumerable<EspecialidadDto>>(lista);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<EspecialidadDto>> ObtenerActivos()
        {
            try
            {
                var lista = await _unidadTrabajo.Especialidad.ObtenerTodos(x=>x.Estado==true,
                    orderBy: e => e.OrderBy(e => e.NombreEspecialidad));
                return _mapper.Map<IEnumerable<EspecialidadDto>>(lista);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
