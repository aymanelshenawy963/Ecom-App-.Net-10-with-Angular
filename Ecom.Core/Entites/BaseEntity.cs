using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entites;

public class BaseEntity<T>
{
    public required T Id { get; set; }
}
