

using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class Editar : PageModel
   {
        private readonly IUnitOfWork _unitOfWork;

        public Editar(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Categoria categoria { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            categoria = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == id);
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

            var categoriaBd = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == categoria.Id);
            if (categoriaBd == null)
            {
                return NotFound();
            }
            //Actualizamos campos modificables
            categoriaBd.Nombre = categoria.Nombre;
            categoriaBd.OrdenVisualizacion = categoria.OrdenVisualizacion;
            //Guardar Cambios
            _unitOfWork.Save();
            TempData["Success"] = "Categoria editada con exito";
            return RedirectToPage("Index");
        }
    }
}
