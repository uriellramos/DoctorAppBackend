﻿using AutoMapper;
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
    public class MedicoServicio : IMedicoServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;

        public MedicoServicio(IUnidadTrabajo unidadTrabajo, IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
        }

        public async Task<MedicoDTO> Agregar(MedicoDTO modeloDto)
        {
            try
            {
                Medico medico = new Medico
                {
                    Apellidos = modeloDto.Apellidos,
                    Nombres = modeloDto.Nombres,
                    Direccion = modeloDto.Direccion,
                    Telefono = modeloDto.Telefono,
                    Genero = modeloDto.Genero,
                    EspecialidadId = modeloDto.EspecialidadId,
                    Estado = modeloDto.Estado == 1 ? true : false,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                };
                await _unidadTrabajo.Medico.Agrgar(medico);
                await _unidadTrabajo.Guardar();
                if (medico.Id == 0)
                    throw new TaskCanceledException("El medico no se pudo crear");
                return _mapper.Map<MedicoDTO>(medico);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Actualizar(MedicoDTO modeloDto)
        {
            try
            {
                var medicoDb = await _unidadTrabajo.Medico.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (medicoDb == null)
                    throw new TaskCanceledException("El medico no existe");
                medicoDb.Apellidos = modeloDto.Apellidos;
                medicoDb.Nombres = modeloDto.Nombres;
                medicoDb.Estado = modeloDto.Estado == 1 ? true : false;
                medicoDb.Direccion = modeloDto.Direccion;
                medicoDb.Telefono = modeloDto.Telefono;
                medicoDb.Genero = modeloDto.Genero;
                medicoDb.EspecialidadId = modeloDto.EspecialidadId;
                _unidadTrabajo.Medico.Actializar(medicoDb);
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
                var medicoDb = await _unidadTrabajo.Medico.ObtenerPrimero(e => e.Id == id);
                if (medicoDb == null)
                    throw new TaskCanceledException("El medico no existe");
                _unidadTrabajo.Medico.Remover(medicoDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MedicoDTO>> ObtenerTodos()
        {
            try
            {
                var lista  = await _unidadTrabajo.Medico.ObtenerTodos(incluirPropiedades:"Especialidad",
                    orderBy: e=> e.OrderBy(e  => e.Apellidos));
                return _mapper.Map<IEnumerable<MedicoDTO>>(lista);

            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
