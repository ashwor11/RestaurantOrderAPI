using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Rules
{
    public class DrinkBusinessRules
    {
        public void DoesDrinkExists(Drink drink) { if (drink == null) throw new BusinessException("The specified drink does not exists."); }
    }
}
