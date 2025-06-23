using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class CategoriaRepository : Repository<Categoria>,IcategoriaRepository
    {


        private readonly ApplicationDbContext _context;
        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Categoria categoria)
        {
            var objDesdeBd = _context.Categorias.FirstOrDefault(x => x.Id == categoria.Id);
            objDesdeBd.Nombre = categoria.Nombre;
            objDesdeBd.OrdenVisualizacion = categoria.OrdenVisualizacion;
        }
    }
}
