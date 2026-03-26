using Ecom.Core.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO;

public record OrderDTO
{
    public int deliveyMethodId { get; set; }
    public string basketId { get; set; }
    public ShippAddressDTO shippingAddress { get; set; }

}

public record ShippAddressDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}