using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Product : Entity 
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Calories { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


    }
}
