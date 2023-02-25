using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.FoodCategories.Rules;

public class FoodCategoryBusinessRules
{
    public void DoesFoodCategoryExist(FoodCategory foodCategory)
    {
        if (foodCategory == null) throw new BusinessException("The specified food category does not exist.");
    }
}