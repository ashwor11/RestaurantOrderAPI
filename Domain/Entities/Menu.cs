using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Menu : Entity
    {
        public int RestaurantId { get; set; }
        public ICollection<FoodCategory> Foods { get; set; }
        public ICollection<DrinkCategory> Drinks { get; set; }
        public virtual Restaurant Resturant { get; set; }

        public Menu()
        {
            Foods = new HashSet<FoodCategory>();
            Drinks = new HashSet<DrinkCategory>();
        }
    }
}
