using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class ProductoRepository : Repository<Producto>,IProductoRepository
    {


        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       

        public void Update(Producto producto)
        {
            var objDesdeBd = _context.Productos.FirstOrDefault(x => x.Id == producto.Id);
            objDesdeBd.Nombre = producto.Nombre;
            objDesdeBd.Descripcion = producto.Descripcion;
            objDesdeBd.Precio = producto.Precio;
            objDesdeBd.CantidadDisponible = producto.CantidadDisponible;
            objDesdeBd.CategoriaId = producto.CategoriaId;
            objDesdeBd.Imagen = producto.Imagen;

        }
    }
}
