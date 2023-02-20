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
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual Restaurant Resturant { get; set; }
    }
}
