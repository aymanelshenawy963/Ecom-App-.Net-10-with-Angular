using Ecom.Core.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.DTO;

public record OrderToReturnDTO
{
    public int Id { get; init; }
    public string BuyerEmail { get; init; }
    public decimal Subtotal { get; init; }
    public decimal Total { get; init; }
    public DateTime OrderDate { get; init; }
    public string Status { get; init; }

    public string DeliveryMethod { get; init; }
    public ShippingAddress ShippingAddress { get; init; }
    public List<OrderItemDTO> OrderItems { get; init; }
}

public class OrderItemDTO
{
    public int ProductItemId { get; init; }
    public string MainImage { get; init; }
    public string ProductName { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}