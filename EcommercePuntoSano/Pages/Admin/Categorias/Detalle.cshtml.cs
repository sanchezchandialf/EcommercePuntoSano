
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class DetalleModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        public DetalleModel(IUnitOfWork unitOfWork)
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
    }
}
