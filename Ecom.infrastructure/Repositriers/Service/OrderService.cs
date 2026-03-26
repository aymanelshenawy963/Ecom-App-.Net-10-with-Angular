using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Order;
using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Repositriers.Service;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public OrderService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _mapper = mapper;
    }

    public async Task<Orders> CreateOrderAsync(OrderDTO orderDTO, string buyerEmail)
    {
        var basket = await _unitOfWork.CustomerBasket.GetBasketAsync(orderDTO.basketId);
        var orderItems = new List<OrderItem>();
        foreach (var item in basket.BasketItems)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
            var orderItem = new OrderItem(product.Id, item.Image, product.Name, item.Price, item.Quantity);
            orderItems.Add(orderItem);
        }

        var deliveryMethod =await _context.DeliveryMethods.FirstOrDefaultAsync(m=>m.Id==orderDTO.deliveyMethodId);
        var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
        var shippingAddress = _mapper.Map<ShippingAddress>(orderDTO.shippingAddress);
        var order = new Orders(buyerEmail,subTotal,shippingAddress ,deliveryMethod!,orderItems);

        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
        await _unitOfWork.CustomerBasket.DeleteBasketAsync(orderDTO.basketId);
        return order;

    }

    public async Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string buyerEmail)
    {
        var orders = await _context.Orders.Where(o => o.BuyerEmail == buyerEmail)
                                    .Include(o => o.OrderItems)
                                    .Include(o => o.DeliveryMethod)
                                    .ToListAsync();
        var result = _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);

        return result;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()=>
       await _context.DeliveryMethods.AsNoTracking().ToListAsync();


    public async Task<OrderToReturnDTO> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var order = await _context.Orders.Where(o => o.Id == id && o.BuyerEmail == buyerEmail)
                                   .Include(o => o.OrderItems)
                                   .Include(o => o.DeliveryMethod)
                                   .FirstOrDefaultAsync();
        var result = _mapper.Map<OrderToReturnDTO>(order);
        return result;
    }
}
