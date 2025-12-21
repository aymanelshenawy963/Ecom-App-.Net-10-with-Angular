using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.interfaces;

public interface IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IPhotoRepository PhotoRepository { get; }
}
