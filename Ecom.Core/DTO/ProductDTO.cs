using Ecom.Core.Entites.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO;

public record ProductDTO
{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }

        public virtual List<PhotoDTO> Photos { get; set; } 
        public string CategoryName{ get; set; } = string.Empty;
}

public record ReturnProductDTO
{
    public List<ProductDTO> Products { get; set; }
    public int TotalCount { get; set; }
}
public record PhotoDTO
{
    public string ImageName { get; set; } = string.Empty;
    public int ProductId { get; set; }
}

public record AddProductDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal NewPrice { get; set; }
    public decimal OldPrice { get; set; } 

    public int CategoryId { get; set; }

    public IFormFileCollection Photos { get; set; }

}

public record UpdateProductDTO : AddProductDTO
{
    public int Id { get; set; }
}