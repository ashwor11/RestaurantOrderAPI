using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderRepository : EfRepositoryBase<RestaurantDbContext,Order>, IOrderRepository
{
    public OrderRepository(RestaurantDbContext context) : base(context)
    {
    }
}