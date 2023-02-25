﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Dtos
{
    public class FoodCategoryForListingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FoodForListingDto> Foods{ get; set; }
    }
}
