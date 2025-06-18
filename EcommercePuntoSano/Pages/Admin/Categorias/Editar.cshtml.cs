
using Ecommerce.DataAccess;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class Editar : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Editar(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var categoriaBd = await _context.Categorias.FindAsync(categoria.Id);
            if (categoriaBd == null)
            {
                return NotFound();
            }
            //Actualizamos campos modificables
            categoriaBd.Nombre = categoria.Nombre;
            categoriaBd.OrdenVisualizacion = categoria.OrdenVisualizacion;
            //Guardar Cambios
            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoria editada con exito";
            return RedirectToPage("Index");
        }
    }
}
