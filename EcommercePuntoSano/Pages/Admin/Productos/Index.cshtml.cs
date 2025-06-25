using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ecommerce.DataAccess;
using Ecommerce.Models;

namespace EcommercePuntoSano.Pages.Admin.Productos
{
    public class IndexModel : PageModel
    {
        private readonly Ecommerce.DataAccess.ApplicationDbContext _context;

        public IndexModel(Ecommerce.DataAccess.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Producto = await _context.Productos
                .Include(p => p.Categoria).ToListAsync();
        }
    }
}
