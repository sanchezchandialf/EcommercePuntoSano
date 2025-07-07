using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository.Irepository
{
    public interface IProductoRepository : IRepository<Producto> 
    {
       
        //Si se quiere actualizar una categoria
        void Update(Producto producto);
        
    }
}
