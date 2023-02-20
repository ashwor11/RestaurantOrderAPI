using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CrossTableEntities
{
    public class CampaignFoods : Entity
    {
        public int CampaignId { get; set; }
        public int FoodId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Food Food { get; set; }
    }
}
