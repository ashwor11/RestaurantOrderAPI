namespace Application.Features.Tables.Dtos;

public class CurrentOrderDto
{
    public ICollection<OrderedProductInOrderDto> Products { get; set; }
    public int TotalPrice { get; set; }
    public int PaidPrice { get; set; }
}