using MediatR;

namespace Application.Features.Categories.Dtos;

public class GetFoodCategoryByIdDto : IRequest<GetFoodCategoryByIdDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MenuId { get; set; }
    public ICollection<GetFoodForCategoryDto> Foods { get; set; }
}

public class GetFoodForCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Calories { get; set; }
    public string Explanation { get; set; }
}