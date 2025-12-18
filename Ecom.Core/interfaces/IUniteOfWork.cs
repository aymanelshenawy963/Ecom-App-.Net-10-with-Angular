using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.interfaces;

public interface IUniteOfWork
{
    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IPhotoRepository PhotoRepository { get; }
}
