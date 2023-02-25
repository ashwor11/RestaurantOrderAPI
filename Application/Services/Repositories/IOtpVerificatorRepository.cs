using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IOtpVerificatorRepository : IRepository<OtpVerificator>, IAsyncRepository<OtpVerificator>
{
    
}