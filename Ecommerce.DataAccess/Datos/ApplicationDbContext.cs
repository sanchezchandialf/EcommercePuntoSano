using Ecommerce.Models;

using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<Categoria> Categorias { get; set; } 
       

    }
}
