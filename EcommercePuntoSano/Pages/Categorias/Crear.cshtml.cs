using EcommercePuntoSano.Datos;
using EcommercePuntoSano.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Categorias
{
    public class CrearModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CrearModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Categoria categoria { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            //Validacion personalizada 
            bool nombreExiste=_context.Categorias.Any(c => c.Nombre == categoria.Nombre);
            if (nombreExiste)
            {
                ModelState.AddModelError("categoria.nombre", "el nombre de la categoria ya existe, porfavor elige otro ");
          
                return Page();
            }   
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //asignar la fecha de creacion
            categoria.FechaCreacion = DateTime.Now;

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            //Usar TempDATA ,muestr el mensaje en la pagina de indice
            TempData["Success"] = "Categoria creada con existo";
            return RedirectToPage("Index");
        }
    }
}
