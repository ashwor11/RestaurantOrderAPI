using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OrderedProductRepository : EfRepositoryBase<RestaurantDbContext,OrderedProduct>, IOrderedProductRepository
{
    public OrderedProductRepository(RestaurantDbContext context) : base(context)
    {
    }
}