using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AbstractEntities
{
    public abstract class Category: Entity 
    {
        public string Name { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        
    }
}
