using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.Entidades;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace API.Extenciones
{
    public static class ServicioIdentidadExtension
    {
        public static IServiceCollection AgregarServicioIdentidad(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<UsuarioAplicacion>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddRoles<RolAplicacion>().AddRoles<RolAplicacion>().AddRoleManager<RoleManager<RolAplicacion>>().AddEntityFrameworkStores<ApplicationDbContext>();                ;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>

                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRol", policy => policy.RequireRole("Admin"));
                options.AddPolicy("AdminAgendadorRol", policy => policy.RequireRole("Admin", "Agendador"));
                options.AddPolicy("AdminDoctorRol", policy => policy.RequireRole("Admin", "Doctor"));
            });
            return services;
        }

    }
}
