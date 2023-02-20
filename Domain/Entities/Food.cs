using Domain.Entities.CrossTableEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Food : Product
    {
        public virtual ICollection<CampaignFoods> Campaigns { get; set; }

    }
}
