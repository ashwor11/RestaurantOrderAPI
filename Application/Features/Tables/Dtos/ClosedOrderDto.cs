namespace Application.Features.Tables.Dtos;

public class ClosedOrderDto
{
    public ICollection<OrderedProductInOrderDto> Products { get; set; }
    public int TotalPrice { get; set; }
}