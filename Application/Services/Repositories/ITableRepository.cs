using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface ITableRepository : IAsyncRepository<Table>, IRepository<Table>
{
    
}