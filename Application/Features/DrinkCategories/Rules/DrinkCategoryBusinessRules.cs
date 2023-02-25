using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DrinkCategories.Rules
{
    public class DrinkCategoryBusinessRules
    {
        public void DoesDrinkCategoryExists(DrinkCategory drinkCategory) { if (drinkCategory == null) throw new BusinessException("Specified drink category does not exists."); }
    }
}
