using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RefreshTokenRepository : EfRepositoryBase<RestaurantDbContext,RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(RestaurantDbContext context) : base(context)
    {
    }
}