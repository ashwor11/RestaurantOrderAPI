using Core.Persistence.Repositories;
using Domain.Entities.CrossTableEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Campaign : Entity
    {
        public double Price { get; set; }
        public int MenuId { get; set; }
        public virtual ICollection<CampaignFoods> Foods{ get; set; }
        public virtual ICollection<CampaignDrinks> Drinks { get; set; }
        public virtual Menu Menu{ get; set; }

    }
}
