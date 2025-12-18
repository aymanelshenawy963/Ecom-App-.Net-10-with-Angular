using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers;

public class ProductRepository : GenericRepositry<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
