using EcommercePuntoSano.Modelos;
using Microsoft.EntityFrameworkCore;

namespace EcommercePuntoSano.Datos
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Categoria> Categorias { get; set; } 
       

    }
}
