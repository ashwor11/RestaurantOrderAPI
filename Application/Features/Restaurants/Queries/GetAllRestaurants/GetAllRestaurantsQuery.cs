using Application.Features.Restaurants.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Pipelines.Logging;

namespace Application.Features.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : PageRequest, IRequest<GetAllRestaurantsModel>, ILoggableRequest
    {
        public string[] RequiredRoles => new string[] { "admin", "moderator" };

        

        public class GetAllRestaurantsQueryHanlder : IRequestHandler<GetAllRestaurantsQuery, GetAllRestaurantsModel>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMapper _mapper;

            public GetAllRestaurantsQueryHanlder(IRestaurantRepository restaurantRepository, IMapper mapper)
            {
                _restaurantRepository = restaurantRepository;
                _mapper = mapper;
            }

            public async Task<GetAllRestaurantsModel> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Restaurant> restaurants = await _restaurantRepository.GetListAsync();

                GetAllRestaurantsModel model = _mapper.Map<GetAllRestaurantsModel>(restaurants);

                return model;
            }
        }
    }
}
