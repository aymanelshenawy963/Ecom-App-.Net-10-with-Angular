using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers;

public class UniteOfWork : IUniteOfWork
{
    private readonly AppDbContext _context;

    public ICategoryRepository CategoryRepository{ get; }

    public IProductRepository ProductRepository{ get; }

    public IPhotoRepository PhotoRepository{ get; }

    public UniteOfWork(AppDbContext context)
    {
        _context = context;
        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context);
        PhotoRepository = new PhotoRepository(_context);
    }
}
