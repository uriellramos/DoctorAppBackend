using BLL.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Net;
using System.Text.RegularExpressions;

namespace API.Controllers
{
    public class EspecialidadController: BaseAPIController
    {
        private readonly IEspecialidadServicio _especialidadServicio;
        private ApiResponse _response;

        public EspecialidadController(IEspecialidadServicio especialidadServicio)
        {
            _especialidadServicio = especialidadServicio;
            _response = new();
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            try
            {
                _response.Resultado = await _especialidadServicio.ObtenerTodos();
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception Ex)
            {
                _response.IsExitoso = false;
                _response.Mensaje = Ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(EspecialidadDto modeloDto) {
            try
            {
                await _especialidadServicio.Agregar(modeloDto);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception Ex)
            {

                _response.IsExitoso = false;
                _response.Mensaje = Ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(EspecialidadDto especialidadDto) {

            try
            {
                await _especialidadServicio.Actualizar(especialidadDto);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception Ex)
            {

                _response.IsExitoso = false;
                _response.Mensaje = Ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _especialidadServicio.Remover(id);
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception Ex)
            {

                _response.IsExitoso = false;
                _response.Mensaje = Ex.Message;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Ok(_response);
        }
    }
}
