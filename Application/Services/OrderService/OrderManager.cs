using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.OrderService;

public class OrderManager : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderManager(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> AddOrderToDb(Order order)
    {
       return await _orderRepository.CreateAsync(order);
        
    }
}