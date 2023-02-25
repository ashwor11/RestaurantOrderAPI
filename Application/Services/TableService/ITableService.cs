using Domain.Entities;

namespace Application.Services.TableService;

public interface ITableService
{
    public Task DoesRestaurantHaveTableWithThisNo(int restaurantId, int tableId);
    public Table CloseTable(Table table, out Order closedOrder);
}