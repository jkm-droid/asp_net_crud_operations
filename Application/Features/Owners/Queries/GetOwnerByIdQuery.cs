using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Queries
{
    public class GetOwnerByIdQuery : IRequest<OwnerDto>
    {
        public Guid Id { get; set; }
        public bool TrackChanges { get; set; }
        
        public GetOwnerByIdQuery(Guid id, bool trackChanges)
        {
            Id = id;
            TrackChanges = trackChanges;
        }
    }

    internal sealed class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<OwnerDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _repositoryManager.Owner.GetOwnerById(request.Id, request.TrackChanges);
            if (owner is null)
                throw new OwnerNotFoundException(request.Id);

            var ownerDto = _mapper.Map<OwnerDto>(owner);

            return ownerDto;
        }
    }
}