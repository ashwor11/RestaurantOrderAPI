using Application.Features.Tables.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Tables.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Table, CreatedTableDto>().ReverseMap();
        CreateMap<Table, TableInOrderDto>().ReverseMap();
        CreateMap<CurrentOrderDto, Order>().ReverseMap();
        CreateMap<OrderedProductInOrderDto, OrderedProduct>()
            .ForPath(x=>x.Product.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(x=>x.Product.Price,opt => opt.MapFrom(z => z.Price)).ReverseMap();
        CreateMap<OrderedProduct, OrderedProductInOrderDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Product.Price));

        CreateMap<Table, GetTableByIdDto>().ReverseMap();
        CreateMap<OrderDtoForGetTableById, Order>().ReverseMap();
        CreateMap<Order, OrderDtoForGetTableById>().ReverseMap();
        CreateMap<Table, ClosedOrderTableDto>().ReverseMap();
        CreateMap<ClosedOrderDto, Order>().ReverseMap();



    }
}