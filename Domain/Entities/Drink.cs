using Domain.Entities.CrossTableEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Drink : Product
    {
        public int Volume { get; set; }
        public virtual ICollection<CampaignDrinks> Campaigns { get; set; }

    }
}
