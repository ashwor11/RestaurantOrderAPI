namespace Application.Features.Foods.Dtos;

public class GetFoodByIdDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Calories { get; set; }
    public string Explanation { get; set; }
    public bool HasStock { get; set; }
}