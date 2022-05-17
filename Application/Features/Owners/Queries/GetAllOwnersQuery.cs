using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Application.Features.Owners.Queries
{
    public class GetAllOwnersQuery : IRequest<(IEnumerable<OwnerDto> owners, PagingMetaData pagingMetaData)>
    {
        public OwnerParameters OwnerParameters { get; set; }
        public bool TrackChanges { get; set; }
        public GetAllOwnersQuery(OwnerParameters ownerParameters,bool trackChanges)
        {
            TrackChanges = trackChanges;
            OwnerParameters = ownerParameters;
        }
    }
    
    internal sealed class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, (IEnumerable<OwnerDto> owners, PagingMetaData pagingMetaData)>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAllOwnersQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<OwnerDto> owners, PagingMetaData pagingMetaData)> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
                var owners = await _repository.Owner.GetAllOwners(request.OwnerParameters, request.TrackChanges);
                var ownersDto = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                
                return (owners : ownersDto, pagingMetaData: owners.PagingMetaData);
           }
    }
}