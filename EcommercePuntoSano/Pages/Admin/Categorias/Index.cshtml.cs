
using Ecommerce.DataAccess;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class IndexModel : PageModel
    {
      
        private readonly IcategoriaRepository _bdCategoria;
        public IndexModel(IcategoriaRepository bdCategoria)
        {
            _bdCategoria = bdCategoria;
        }

        public IEnumerable<Categoria> Categorias { get; set; } = default!;
    
        public void OnGet()
        {

            Categorias = _bdCategoria.GetAll();


        }

        public async Task<IActionResult> OnPostDeleteAsync([FromBody]int id)
        {
            var categoria = await _bdCategoria.Categorias.FindAsync(id);
            if (categoria == null) {
                TempData["Error"] = "La categoria no fue encontrada";
                return RedirectToPage("Index");

            }
            _bdCategoria.Categorias.Remove(categoria);
            await _bdCategoria.SaveChangesAsync();
            TempData["Success"] = "Categoria Eliminada con exito";
            return new JsonResult(new {success=true});
        }
    }
    
}

