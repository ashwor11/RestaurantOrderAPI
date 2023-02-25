﻿using Domain.Entities.CrossTableEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.AbstractEntities;

namespace Domain.Entities
{
    public class Drink : Product
    {
        public int? Volume { get; set; }
        public int DrinkCategoryId { get; set; }
        public virtual DrinkCategory DrinkCategory { get; set; }

    }
}
