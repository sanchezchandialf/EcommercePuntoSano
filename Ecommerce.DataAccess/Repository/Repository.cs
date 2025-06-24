using Ecommerce.DataAccess.Repository.Irepository;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<T> Dbset;
      

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.Dbset = _context.Set<T>();
        }
        public void Add(T entity)=>Dbset.Add(entity);

        public bool ExisteNombre(string nombre)
        {
            return _context.Categorias.Any(c => c.Nombre == nombre);
        }

        public IEnumerable<T> GetAll()
        {
           IQueryable<T> query = Dbset;
            //Si se quiere incluir propiedades de navegacion
            //query = query.Include(x => x.PropiedadNavegacion);
            //Si se quiere filtrar por una propiedad
            //query = query.Where(x => x.Propiedad == "valor");
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = Dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entity) => Dbset.Remove(entity);

        public void RemoveRange(IEnumerable<T> entity) => Dbset.RemoveRange(entity);



    }
}
