using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.TableService;

public class TableManager : ITableService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public TableManager(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task DoesRestaurantHaveTableWithThisNo(int restaurantId, int tableId)
    {
        Table table =
            (await _restaurantRepository.GetAsync(x => x.Id == restaurantId, x => x.Include(x => x.Tables))).Tables
            .FirstOrDefault(t => t.TableNo == tableId);
        if (table != null) throw new BusinessException("Specified restaurant already has a table with this no.");
    }

    public Table CloseTable(Table table, out Order closedOrder)
    {
        foreach (OrderedProduct currentOrderProduct in table.CurrentOrder.Products)
        {
            if (!currentOrderProduct.IsPaid)
            {
                currentOrderProduct.IsPaid = true;
            }
        }
        //to do payment methods
        table.CurrentOrder.PaidPrice = table.CurrentOrder.TotalPrice;
        table.CurrentOrder.ClosedDate = DateTime.Now;
        table.Orders.Add(table.CurrentOrder);
        table.OrderId = null;
        closedOrder = table.CurrentOrder;   
        return table;
    }
}