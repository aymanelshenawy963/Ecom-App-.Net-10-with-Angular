using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers;

public class CategoryRepository : GenericRepositry<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
