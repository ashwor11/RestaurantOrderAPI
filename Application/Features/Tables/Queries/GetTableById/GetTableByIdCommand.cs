using Application.Features.Tables.Dtos;
using Application.Features.Tables.Rules;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tables.Queries.GetTableById;

public class GetTableByIdCommand : IRequest<GetTableByIdDto>
{
    public int Id { get; set; }


    public class GetTableByIdCommandHandler : IRequestHandler<GetTableByIdCommand, GetTableByIdDto>
    {
        public GetTableByIdCommandHandler(ITableRepository tableRepository, IMapper mapper, TableBusinessRules tableBusinessRules)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _tableBusinessRules = tableBusinessRules;
        }

        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;
        private readonly TableBusinessRules _tableBusinessRules;
        public async Task<GetTableByIdDto> Handle(GetTableByIdCommand request, CancellationToken cancellationToken)
        {
            Table? table = await _tableRepository.GetAsync(x => x.Id == request.Id,
                x => x.Include(x => x.CurrentOrder).ThenInclude(x => x.Products).ThenInclude(x => x.Product)
                    .Include(x => x.Orders).ThenInclude(x => x.Products).ThenInclude(x => x.Product));
            _tableBusinessRules.DoesTableExists(table);

            GetTableByIdDto response = _mapper.Map<GetTableByIdDto>(table);
            return response;
        }
    }
}