using Domain.Entities;
using Domain.Entities.AbstractEntities;

namespace Application.Services.ProductService;

public interface IProductService
{
    public void DoProductsBelongRestaurant(List<Product> products, int restaurantId);
    public Task<List<Product>> GetSpecifiedProducts(List<int> productIds);
    void AddProductsToCurrentOrder(List<Product> products, Table table, List<int> productIds);
    void PayProductsPrice(Table table, List<int> productIds);
}