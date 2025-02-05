using McCapas.Models;
using Microsoft.EntityFrameworkCore;

namespace McCapas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Capas> capas { get; set; }
        public DbSet<Material> materialDeFabricacao { get; set; }
        public DbSet<Tapetes> tapete { get; set; }

        public DbSet<UsuarioModel> usuarios { get; set; }
    }
}
