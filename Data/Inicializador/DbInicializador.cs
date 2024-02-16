using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Inicializador
{
    public class DbInicializador : IdbInicializador
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly RoleManager<RolAplicacion> _rolManager;

        public DbInicializador(ApplicationDbContext db, UserManager<UsuarioAplicacion> userManager, RoleManager<RolAplicacion> rolManager)
        {
            _db = db;
            _userManager = userManager;
            _rolManager = rolManager;
        }

        public async void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count()>0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //Datos iniciales
            if (_db.Roles.Any(r => r.Name == "Admin")) return;

             _rolManager.CreateAsync(new RolAplicacion { Name = "Admin" }).GetAwaiter().GetResult();
             _rolManager.CreateAsync(new RolAplicacion { Name = "Agendador" }).GetAwaiter().GetResult();
             _rolManager.CreateAsync(new RolAplicacion { Name = "Doctor" }).GetAwaiter().GetResult();


            //Crear Usuario Amdministrador
            var usuario = new UsuarioAplicacion
            {
                UserName = "administrador",
                Email = "uriell.ramos@gmail.com",
                Apellidos = "Ramos",
                Nombre = "Uriel",
            };
             _userManager.CreateAsync(usuario, "Admin123").GetAwaiter().GetResult();
            //asignar el Rol Admin al usuario
            UsuarioAplicacion usuarioAdmin = _db.UsuarioAplicacion.Where(u=> u.UserName == "administrador").FirstOrDefault();
            _userManager.AddToRoleAsync(usuarioAdmin, "Admin").GetAwaiter().GetResult();
        }
    }
}
