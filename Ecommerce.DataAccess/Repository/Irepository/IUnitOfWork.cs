﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository.Irepository
{
    public interface IUnitOfWork : IDisposable
    {
        IcategoriaRepository Categoria { get; }
        IProductoRepository Producto { get; }
        void Save();
    }
}
