using Application.Features.Foods.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Profiles
{
    public class FoodMappingProfile:Profile
    {
        public FoodMappingProfile()
        {
            CreateMap<Food, CommandResponseFoodDto>().ReverseMap();
            CreateMap<Food, AddFoodToCategoryDto>().ReverseMap();
        }
    }
}
