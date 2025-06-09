using Microsoft.EntityFrameworkCore;

namespace EcommercePuntoSano.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar tus entidades, relaciones, etc.
        }
        // DbSets para tus entidades
        // public DbSet<Producto> Productos { get; set; }
        // public DbSet<Categoria> Categorias { get; set; }
        // etc.
    }
}
