using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : Entity
    {
        public int Name { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
