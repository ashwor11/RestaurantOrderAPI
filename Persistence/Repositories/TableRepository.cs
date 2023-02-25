using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TableRepository : EfRepositoryBase<RestaurantDbContext, Table>, ITableRepository
{
    public TableRepository(RestaurantDbContext context) : base(context)
    {
    }
}