using Domain.Entities;
using Domain.Entities.AbstractEntities;

namespace Application.Helpers;

public static class TableHelper
{
    public static void AddProductToOrder(this Table table, Product product)
    {
        OrderedProduct orderedProduct = new OrderedProduct(){ProductId = product.Id};
        table.CurrentOrder.Products.Add(orderedProduct);
        table.CurrentOrder.TotalPrice += product.Price;
    }
}