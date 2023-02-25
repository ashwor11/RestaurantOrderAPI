namespace Application.Features.Categories.Dtos;

public class DeletedFoodCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MenuId { get; set; }
    public ICollection<DeletedFoodByDeletionOfCategory> Foods { get; set; }
}

public class DeletedFoodByDeletionOfCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Calories { get; set; }
    public string Explanation { get; set; }
}