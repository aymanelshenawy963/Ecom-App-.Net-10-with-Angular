using AutoMapper;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositriers.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers;

public class UniteOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageMangementService _imageMangementService;

    public ICategoryRepository CategoryRepository{ get; }

    public IProductRepository ProductRepository{ get; }

    public IPhotoRepository PhotoRepository{ get; }

    public UniteOfWork(AppDbContext context, IMapper mapper, IImageMangementService imageMangementService)
    {
        _context = context;
        _imageMangementService = imageMangementService;
        _mapper = mapper;

        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context,mapper,imageMangementService);
        PhotoRepository = new PhotoRepository(_context);


    }
}
