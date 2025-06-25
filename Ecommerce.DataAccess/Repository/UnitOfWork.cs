using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DataAccess.Repository.Irepository;

namespace Ecommerce.DataAccess.Repository
{
    public  class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categoria = new CategoriaRepository(_context);
            Producto = new ProductoRepository(_context);
        }
        public IcategoriaRepository Categoria { get; private set; }

        public IProductoRepository Producto { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}
