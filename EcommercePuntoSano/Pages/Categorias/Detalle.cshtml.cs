using EcommercePuntoSano.Datos;
using EcommercePuntoSano.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Categorias
{
    public class DetalleModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public DetalleModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Categoria categoria { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
