using Core.Persistence.Repositories;
using Domain.Entities.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DrinkCategory : Category
    {
        public ICollection<Drink> Drinks{ get; set; }
        public DrinkCategory()
        {
            Drinks = new HashSet<Drink>();
        }
    }
}
