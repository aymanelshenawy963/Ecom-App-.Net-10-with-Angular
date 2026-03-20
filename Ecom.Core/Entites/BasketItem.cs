using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entites;

public class BasketItem
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public string Category { get; set; } =string.Empty;
}
