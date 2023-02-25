namespace Application.Features.Tables.Dtos;

public class AddProductToOrderDto
{
    public List<int> ProductIds { get; set; }
    public int TableId { get; set; }
}