using System.Security.AccessControl;
using Core.Persistence.Repositories;

namespace Domain.Entities.AbstractEntities
{
    public abstract class Product : Entity 
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Explanation { get; set; }
        public int? Calories { get; set; }
        public int RestaurantId { get; set; }
        public bool HasStock { get; set; } = true;

    }
}
