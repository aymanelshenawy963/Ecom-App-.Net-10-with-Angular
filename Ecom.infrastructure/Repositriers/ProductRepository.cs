using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ecom.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecom.infrastructure.Repositriers;

public class ProductRepository : GenericRepositry<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IImageMangementService _imageMangementService;

    public ProductRepository(AppDbContext context, IMapper mapper,IImageMangementService imageMangementService) : base(context)
    {
        _mapper = mapper;
        _imageMangementService = imageMangementService;
        _context = context;
    }

    public async Task<bool> AddAsync(AddProductDTO productDTO)
    {
        if (productDTO == null) return false;
        var product = _mapper.Map<Product>(productDTO);

       await _context.Products.AddAsync(product);
         await _context.SaveChangesAsync();

        var imagePaths = await _imageMangementService.AddImageAsync(productDTO.Photos, productDTO.Name);

        var photo = imagePaths.Select(path => new Photo
        {
            ImageName = path,
            ProductId = product.Id
        }).ToList();
        await _context.Photos.AddRangeAsync(photo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(UpdateProductDTO productDTO)
    {
        if (productDTO is null) return false;

        var findProduct = await _context.Products.Include(m=>m.Category)
            .Include(m=>m.Photos)
            .FirstOrDefaultAsync(m=>m.Id == productDTO.Id);

        if (findProduct is null) return false;

        _mapper.Map(productDTO, findProduct);

        var findPhotos = await _context.Photos.Where(m => m.ProductId == productDTO.Id).ToListAsync();

        foreach (var photo in findPhotos)
        {
             _imageMangementService.DeleteImageAsync(photo.ImageName);
        }


        _context.Photos.RemoveRange(findPhotos);

        var imagePaths = await _imageMangementService.AddImageAsync(productDTO.Photos, productDTO.Name);

        var photos = imagePaths.Select(path => new Photo
        {
            ImageName = path,
            ProductId = productDTO.Id
        }).ToList();

        await _context.Photos.AddRangeAsync(photos);
        await _context.SaveChangesAsync();

        return true;

    }

    public async Task DeleteAsync(Product product)
    {
        var photos = await _context.Photos.Where(m => m.ProductId == product.Id).ToListAsync();
        foreach (var photo in photos)
        {
            _imageMangementService.DeleteImageAsync(photo.ImageName);
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
