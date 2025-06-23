using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository.Irepository
{
     public interface IRepository<T> where T : class
    {
        //Métodos para CRUD. Leer registros, registro individual
        //Crear registro, Borrar y Borrar masive
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        IEnumerable<T> GetAll();
        T GetFirstOrDefault(Expression <Func<T,bool>> ? filter=null);

        bool ExisteNombre(string  nombre);
    }
}
