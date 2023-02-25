using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Rules
{
    public class FoodBusinessRules
    {
        public void DoesFoodExists(Food food) { if (food == null) throw new BusinessException("The specified food does not exists."); }
    }
}
