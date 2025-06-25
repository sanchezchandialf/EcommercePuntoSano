

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.DataAccess;
using Ecommerce.Models;
using Ecommerce.DataAccess.Repository.Irepository;

namespace EcommercePuntoSano.Pages.Admin.Productos
{
    public class CreateModel : PageModel
    {
        private IUnitOfWork _unitOfWork;
        
        
        [BindProperty]
        public Producto Producto { get; set; } = default!;

        public IEnumerable<SelectListItem> Categorias { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet()
        {
            Categorias = _unitOfWork.Categoria.GetAll()
                   .Select(c => new SelectListItem
                   {
                       Value = c.Id.ToString(),
                       Text = c.Nombre
                   });

            //Validación por si la tabla categorías no tiene ni una sola categoría creada
            if (!Categorias.Any())
            {
                ModelState.AddModelError(string.Empty, "No hay categorías disponibles. Por favor, agregue categorías primero.");
            }

            return Page();

        }




        public async Task<IActionResult> OnPostAsync()
        {
            
            if (_unitOfWork.Producto.ExisteNombre(Producto.Nombre))
            {
                ModelState.AddModelError("Producto.Nombre","El nombre del producto ya existe. Por favor elige otro");
                return Page();
            
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Producto.FechaCreacion = DateTime.Now;
            _unitOfWork.Producto.Add(Producto);
            _unitOfWork.Save();

            // Redireccionar después de guardar
            return RedirectToPage("Index"); // Cambiá "Index" por la página que quieras mostrar después
        }
    }
}
