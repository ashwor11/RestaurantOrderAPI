using Application.Services.Repositories;
using Domain.Entities.AbstractEntities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    protected RestaurantDbContext Context { get; }

    public ProductRepository(RestaurantDbContext context)
    {
        Context = context;
    }
    public async Task<Product>? GetProductByIdAsync(int id)
    {

        Product ?product = await Context.Drinks.Where(e => e.Id == id).Cast<Product>()
            .Concat(Context.Foods.Where(x => x.Id == id).Cast<Product>()).FirstOrDefaultAsync();
        return product;
    }


    public async Task<List<Product>> GetProductsByIdsAsync(List<int> ids)
    {
        List<Product> products = (await Context.Drinks
                .Where(e => ids.Contains(e.Id))
                .Cast<Product>()
                .ToListAsync())
            .AsEnumerable()
            .Concat(await Context.Foods
                .Where(e => ids.Contains(e.Id))
                .Cast<Product>()
                .ToListAsync())
            .ToList();
        return products;
    }
}