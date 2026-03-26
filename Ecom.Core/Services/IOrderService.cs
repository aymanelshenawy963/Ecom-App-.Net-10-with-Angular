using Ecom.Core.DTO;
using Ecom.Core.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Services;

public interface IOrderService
{
    Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string buyerEmail);
    Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string buyerEmail);
    Task<OrderToReturnDTO> GetOrderByIdAsync(int id, string buyerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();


}
