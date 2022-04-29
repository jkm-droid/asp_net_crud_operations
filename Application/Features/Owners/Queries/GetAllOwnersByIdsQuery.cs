using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Queries
{
    public class GetAllOwnersByIdsQuery : IRequest<IEnumerable<OwnerDto>>
    {
        public GetAllOwnersByIdsQuery(IEnumerable<Guid> ownerIds, bool trackChanges)
        {
            this.OwnerIds = ownerIds;
            TrackChanges = trackChanges;
        }

        public IEnumerable<Guid> OwnerIds { get; set; }
        public bool TrackChanges { get; set; }
    }

    internal sealed class GetAllOwnersByIdsQueryHandler : IRequestHandler<GetAllOwnersByIdsQuery, IEnumerable<OwnerDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetAllOwnersByIdsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OwnerDto>> Handle(GetAllOwnersByIdsQuery request, CancellationToken cancellationToken)
        {
            if (request.OwnerIds is null)
                throw new IdParametersBadRequestException();
            
            var owners = await _repositoryManager.Owner.GetOwnersByIds(request.OwnerIds, request.TrackChanges);
            if (request.OwnerIds.Count() != owners.Count())
                throw new CollectionByIdsBadRequestException();

            var responseOwners = _mapper.Map<IEnumerable<OwnerDto>>(owners);

            return responseOwners;

        }
    }
}