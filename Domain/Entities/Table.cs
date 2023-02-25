using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Table : Entity
    {
        public int TableNo { get; set; }
        public int? OrderId { get; set; } = null;
        public int RestaurantId { get; set; }
        public virtual Order? CurrentOrder { get; set; } = null;
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Table()
        {
            Orders = new HashSet<Order>();
        }

    }
}
