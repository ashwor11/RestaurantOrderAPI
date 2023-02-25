using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Categories.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.FoodCategories.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoodCategory, DeletedFoodCategoryDto>().ReverseMap();
            CreateMap<Food,DeletedFoodByDeletionOfCategory>().ReverseMap();
            CreateMap<FoodCategory,GetFoodCategoryByIdDto>().ReverseMap();
            CreateMap<Food,GetFoodForCategoryDto>().ReverseMap();
        }
    }
}
