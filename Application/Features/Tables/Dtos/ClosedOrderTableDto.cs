namespace Application.Features.Tables.Dtos;

public class ClosedOrderTableDto
{
    public int Id { get; set; }
    public int TableNo { get; set; }
    public int RestaurantId { get; set; }
    public int ClosedOrderId { get; set; }
    public ClosedOrderDto ClosedOrder { get; set; }
    

}