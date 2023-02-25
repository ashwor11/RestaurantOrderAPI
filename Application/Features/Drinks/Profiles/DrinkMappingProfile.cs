using Application.Features.Drinks.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Profiles
{
    public class DrinkMappingProfile: Profile
    {
        public DrinkMappingProfile()
        {
            CreateMap<Drink, AddDrinkToCategoryDto>().ReverseMap();
            CreateMap<CommandResponseDrinkDto, Drink>().ReverseMap();
            CreateMap<Drink, DrinkGetByIdDto>().ReverseMap();
        }
    }
}
