using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers;

public class PhotoRepository : GenericRepositry<Photo>, IPhotoRepository
{
    public PhotoRepository(AppDbContext context) : base(context)
    {
    }
}
