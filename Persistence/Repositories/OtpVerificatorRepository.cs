using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OtpVerificatorRepository : EfRepositoryBase<RestaurantDbContext,OtpVerificator>, IOtpVerificatorRepository
{
    public OtpVerificatorRepository(RestaurantDbContext context) : base(context)
    {
    }
}