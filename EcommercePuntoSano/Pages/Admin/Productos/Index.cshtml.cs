using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EcommercePuntoSano.Pages.Admin.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public IEnumerable <Producto> Productos { get;set; } = default!;
        public void OnGet()
        {
            //Cargar todas las categorias desde la base de datos
            Productos = _unitOfWork.Producto.GetAll("Categoria");
        }

    }
}
