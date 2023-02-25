using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IEmailVertificatorRepository : IRepository<EmailVertificator>, IAsyncRepository<EmailVertificator>
{
    
}