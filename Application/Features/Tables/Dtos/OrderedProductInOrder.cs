namespace Application.Features.Tables.Dtos;

public class OrderedProductInOrderDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public bool IsPaid { get; set; }
    
}