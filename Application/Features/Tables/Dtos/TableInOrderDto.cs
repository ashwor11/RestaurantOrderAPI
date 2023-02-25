using Domain.Entities;

namespace Application.Features.Tables.Dtos;

public class TableInOrderDto
{
    public int Id { get; set; }
    public int TableNo { get; set; }
    public int OrderId { get; set; }
    public int RestaurantId { get; set; }
    public CurrentOrderDto CurrentOrder{ get; set; }

}