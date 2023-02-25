using Application.Services.TableService;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Domain.Entities.AbstractEntities;

namespace Application.Features.Tables.Rules;

public class TableBusinessRules
{
    private readonly ITableService _tableService;

    public TableBusinessRules(ITableService tableService)
    {
        _tableService = tableService;
    }

    public async Task DoesRestaurantHaveAnTableWithThisNo(int restaurantId, int tableNo)
    {
        await _tableService.DoesRestaurantHaveTableWithThisNo(restaurantId, tableNo);
    }

    public void DoProductsBelongRestaurant(int restaurantId, IList<Product> products)
    {
        products.ToList().ForEach(x => {
            if (x.RestaurantId != restaurantId)
            {
                throw new BusinessException("At least one of the products does not belong to this restaurant.");
            }
        });
    }

    public void DoesTableExists(Table table)
    {
        if (table == null) throw new BusinessException("This table does not exist.");
    }

    public void DoesTableHaveAnOpenOrder(Table table)
    {
        if (table.CurrentOrder == null) throw new BusinessException("This table does not have an open order.");
    }

    public void IsProductOrdered(OrderedProduct orderedProduct)
    {
        if (orderedProduct == null) throw new BusinessException("This ordered product does not exist.");
    }
}