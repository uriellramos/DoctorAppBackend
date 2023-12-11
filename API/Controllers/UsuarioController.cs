using Azure.Identity;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Models.DTOs;
using Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{

    public class UsuarioController : BaseAPIController
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenServicio _tokenServicio;
        public UsuarioController(ApplicationDbContext db, ITokenServicio tokenServicio)
        {
            _db = db;
            _tokenServicio = tokenServicio;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuario = await _db.Usuarios.ToListAsync();

            return Ok(usuario);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _db.Usuarios.FindAsync(id);
            return Ok(usuario);
        }
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("UserName ya esta Registrado");
            }

            using var hmac = new HMACSHA512();

            var usuario = new Usuario
            {
                Username = registroDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registroDto.Password)),
                PasswordSalt = hmac.Key,
            };
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();

            return new UsuarioDto {
                Username = usuario.Username,
                 Token = _tokenServicio.CrearToken(usuario),
            };

        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> login(LoginDto loginDto)
        {
            var usuario = await _db.Usuarios.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
            if (usuario == null) { return Unauthorized("Usuario no valido"); }
            using var hmac = new HMACSHA512(usuario.PasswordSalt);
            var computeHas = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computeHas.Length; i++)
            {
                if (computeHas[i] != usuario.PasswordHash[i])
                {
                    return Unauthorized("Password no valido");
                }
            }
            return new UsuarioDto {
                Username = usuario.Username,
                Token = _tokenServicio.CrearToken(usuario),

            };

        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _db.Usuarios.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
