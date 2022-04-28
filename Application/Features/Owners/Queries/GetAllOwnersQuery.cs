using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Queries
{
    public class GetAllOwnersQuery : IRequest<IEnumerable<OwnerDto>>
    {
        public bool TrackChanges { get; set; }
        public GetAllOwnersQuery(bool trackChanges)
        {
            TrackChanges = trackChanges;
        }
    }
    
    internal sealed class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, IEnumerable<OwnerDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly IMapper _mapper;

        public GetAllOwnersQueryHandler(IRepositoryManager repository, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
                var owners = await _repository.Owner.GetAllOwners(request.TrackChanges);
                var ownersDto = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                
                return ownersDto;
           }
    }
}