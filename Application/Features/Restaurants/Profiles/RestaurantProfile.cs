using Application.Features.Restaurants.Dtos;
using Application.Features.Restaurants.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Profiles
{
    public  class RestaurantProfile: Profile
    {
        public RestaurantProfile()
        {
            CreateMap<IPaginate<Restaurant>, GetAllRestaurantsModel>()
                                .ForMember(dest => dest.Restaurants, opt => opt.MapFrom(src => src.Items)).ReverseMap();
                                

            CreateMap<Restaurant, GetRestaurantDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantWithDetailsDto>().ReverseMap();
            CreateMap<Menu, MenuForListingDto>()
                                            .ForMember(dest=> dest.FoodCategories, opt => opt.MapFrom(src => src.Foods))
                                            .ForMember(dest=> dest.DrinkCategories, opt => opt.MapFrom(src => src.Drinks)).ReverseMap();
            CreateMap<DrinkCategoryForListingDto, DrinkCategory>().ReverseMap();
            CreateMap<FoodCategoryForListingDto, FoodCategory>().ReverseMap();
            CreateMap<FoodForListingDto, Food>().ReverseMap();
            CreateMap<DrinkForListingDto, Drink>().ReverseMap();


                                
        }
        
    }
}
