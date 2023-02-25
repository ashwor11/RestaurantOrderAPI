using Application.Features.DrinkCategories.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.DrinkCategories.Profiles;

public class DrinkCategoryProfile : Profile
{
    public DrinkCategoryProfile()
    {
        CreateMap<Drink, DeletedDrinkByDeletionOfCategoryDto>().ReverseMap();
        CreateMap<DrinkCategory, DeletedDrinkCategoryDto>().ReverseMap();
        CreateMap<DrinkCategory, DrinkCategoryGetByIdDto>().ReverseMap();
        CreateMap<Drink, GetDrinkCategoryByIdDrinkDto>().ReverseMap();
    }
}