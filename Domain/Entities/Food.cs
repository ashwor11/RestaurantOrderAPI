using Domain.Entities.CrossTableEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AbstractEntities;

namespace Domain.Entities
{
    public class Food : Product
    {
        public int FoodCategoryId { get; set; }
        public virtual FoodCategory FoodCategory { get; set; }
    }
}
