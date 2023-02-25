using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmailVertificatorRepository : EfRepositoryBase<RestaurantDbContext,EmailVertificator> , IEmailVertificatorRepository
{
    public EmailVertificatorRepository(RestaurantDbContext context) : base(context)
    {
    }
}