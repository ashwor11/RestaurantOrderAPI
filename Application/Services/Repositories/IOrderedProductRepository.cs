using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IOrderedProductRepository : IAsyncRepository<OrderedProduct>, IRepository<OrderedProduct>
{
    
}