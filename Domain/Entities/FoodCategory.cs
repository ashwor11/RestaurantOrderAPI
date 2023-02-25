using Domain.Entities.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FoodCategory : Category    {
        public ICollection<Food> Foods { get; set; }
        public FoodCategory()
        {
            Foods = new HashSet<Food>();
        }
    }
}
