using EcommercePuntoSano.Datos;
using EcommercePuntoSano.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EcommercePuntoSano.Pages.Categorias
{
    public class IndexModel : PageModel
    {
      
            private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Categoria> Categorias { get; set; } = default!;
    
        public async Task Onget()
        {
         
               Categorias = await _context.Categorias
                .OrderBy(c => c.OrdenVisualizacion)
                .ToListAsync();


        }

    }
    
}

