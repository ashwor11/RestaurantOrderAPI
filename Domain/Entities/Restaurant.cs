﻿using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual ICollection<Table> Tables { get; set; }

        public Restaurant()
        {
            Tables = new HashSet<Table>();
        }
    }
}
