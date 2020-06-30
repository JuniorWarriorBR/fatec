using Microsoft.EntityFrameworkCore;
using SemTumultoApi.Models.Empresas;
using SemTumultoApi.Models.Reservas;
using SemTumultoApi.Models.Usuarios;

namespace SemTumultoApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
