using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.interfaces;

public interface IProductRepository : IGenericRepositry<Product>
{
    // for future custom product methods
    Task<ReturnProductDTO> GetAllAsync(ProductParams productParams);
    Task<bool> AddAsync(AddProductDTO productDTO);
    Task<bool> UpdateAsync(UpdateProductDTO productDTO);
    Task DeleteAsync(Product product);
}
