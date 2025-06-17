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

        public async Task<IActionResult> OnPostDeleteAsync([FromBody]int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) {
                TempData["Error"] = "La categoria no fue encontrada";
                return RedirectToPage("Index");

            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoria Eliminada con exito";
            return new JsonResult(new {success=true});
        }
    }
    
}

