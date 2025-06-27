
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class CrearModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;


        public CrearModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            if (_unitOfWork.Categoria.ExisteNombre(categoria.Nombre))
            {
                ModelState.AddModelError("Categoria.Nombre", "El nombre de la categoría ya existe. Por favor elige otro.");
                return Page();
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

        
            //asignar la fecha de creacion
            categoria.FechaCreacion = DateTime.Now;

            _unitOfWork.Categoria.Add(categoria);
            _unitOfWork.Save();

            //Usar TempDATA ,muestr el mensaje en la pagina de indice
            TempData["Success"] = "Categoria creada con existo";
            return RedirectToPage("Index");
        }
    }
}
