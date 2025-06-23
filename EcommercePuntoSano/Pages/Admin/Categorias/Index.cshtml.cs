
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace EcommercePuntoSano.Pages.Admin.Categorias
{
    public class IndexModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Categoria> Categorias { get; set; } = default!;
    
        public void OnGet()
        {

            Categorias = _unitOfWork.Categoria.GetAll();


        }

        public async Task<IActionResult> OnPostDeleteAsync([FromBody]int id)
        {
            var categoria = _unitOfWork.Categoria.GetFirstOrDefault(c => c.Id == id);
            if (categoria == null) {
                TempData["Error"] = "La categoria no fue encontrada";
                return RedirectToPage("Index");

            }
            _unitOfWork.Categoria.Remove(categoria);
            _unitOfWork.Save();
            TempData["Success"] = "Categoria Eliminada con exito";
            return new JsonResult(new {success=true});
        }
    }
    
}

