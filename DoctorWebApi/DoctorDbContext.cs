using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace DoctorWebApi
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
