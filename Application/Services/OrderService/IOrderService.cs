using Domain.Entities;

namespace Application.Services.OrderService;

public interface IOrderService
{
    public Task<Order> AddOrderToDb(Order order);
}