using Application.Features.Restaurants.Dtos;
using Application.Features.Restaurants.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Pipelines.Caching;

namespace Application.Features.Restaurants.Queries.GetRestaurantWithAllDetails
{
    public class GetRestaurantWithAllDetailsQuery: IRequest<RestaurantWithDetailsDto>, ICacheableRequest
    {
        public int RestaurantId { get; set; }

        public bool BypassCache { get; set; }
        public string CacheKey  => $"RestaurantId:{RestaurantId}";
        public int SlidingExpiration { get; set; }

        

        public class GetRestaurantWithAllDetailQueryHandler : IRequestHandler<GetRestaurantWithAllDetailsQuery, RestaurantWithDetailsDto>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly RestaurantBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public GetRestaurantWithAllDetailQueryHandler(IRestaurantRepository restaurantRepository, RestaurantBusinessRules businessRules, IMapper mapper)
            {
                _restaurantRepository = restaurantRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<RestaurantWithDetailsDto> Handle(GetRestaurantWithAllDetailsQuery request, CancellationToken cancellationToken)
            {
                Restaurant? restaurant = await _restaurantRepository.GetAsync(x => x.Id == request.RestaurantId, x => x.Include(x => x.Menu).Include(x => x.Menu.Foods).ThenInclude(x=>x.Foods).Include(x=>x.Menu.Drinks).ThenInclude(x=>x.Drinks));
                _businessRules.DoesRestaurantExist(restaurant);

                RestaurantWithDetailsDto response = _mapper.Map<RestaurantWithDetailsDto>(restaurant);
                return response;
            }
        }

        
    }
}
