using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CrossTableEntities
{
    public class CampaignDrinks: Entity
    {
        public int CampaignId { get; set; }
        public int DrinkId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Drink Drink { get; set; }
    }
}
