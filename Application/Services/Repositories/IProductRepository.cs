using Domain.Entities.AbstractEntities;

namespace Application.Services.Repositories;

public interface IProductRepository
{
    public Task<Product> GetProductByIdAsync(int id);
    public Task<List<Product>> GetProductsByIdsAsync(List<int> ids);
}