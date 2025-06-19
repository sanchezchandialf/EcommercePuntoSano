
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class CrearModel : PageModel
    {
        private readonly IcategoriaRepository _bdCategoria;

        public CrearModel(Ic bdCategoria)
        {
            _bdCategoria = bdCategoria;
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
            bool nombreExiste=_bdCategoria.Categorias.Any(c => c.Nombre == categoria.Nombre);
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

            _bdCategoria.Add(categoria);
            await _bdCategoria.Save(categoria);

            //Usar TempDATA ,muestr el mensaje en la pagina de indice
            TempData["Success"] = "Categoria creada con existo";
            return RedirectToPage("Index");
        }
    }
}
