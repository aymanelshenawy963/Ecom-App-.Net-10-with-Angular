using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecom.Core.Entites.Product;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public virtual List<Photo> Photos { get; set; } = default!;
    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; } = default!;
}
