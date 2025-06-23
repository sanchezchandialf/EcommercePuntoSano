using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository.Irepository
{
    public interface IcategoriaRepository : IRepository<Categoria> 
    {
       
        //Si se quiere actualizar una categoria
        void Update(Categoria entity);
        
    }
}
