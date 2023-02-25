namespace Application.Features.Tables.Dtos;

public class GetTableByIdDto
{
    public int Id { get; set; }
    public int TableNo { get; set; }
    public int OrderId { get; set; }
    public int RestaurantId { get; set; }
    public CurrentOrderDto CurrentOrder { get; set; }
    public ICollection<OrderDtoForGetTableById> Orders { get; set; }
}