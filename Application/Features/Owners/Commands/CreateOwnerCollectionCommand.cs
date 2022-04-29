using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Commands
{
    public class CreateOwnerCollectionCommand : IRequest<(IEnumerable<OwnerDto> owners, string ownerIds)>
    {
        public CreateOwnerCollectionCommand(IEnumerable<OwnerCreationDto> ownerCreationDtos)
        {
            OwnerCollections = ownerCreationDtos;
        }

        public IEnumerable<OwnerCreationDto> OwnerCollections { get; set; }
    }

    internal sealed class CreateOwnerCollectionQueryHandler : IRequestHandler<CreateOwnerCollectionCommand, (IEnumerable<OwnerDto> owners, string ownerIds)>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateOwnerCollectionQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<OwnerDto> owners, string ownerIds)> Handle(CreateOwnerCollectionCommand request, CancellationToken cancellationToken)
        {
            if (request.OwnerCollections is null)
                throw new CollectionByIdsBadRequestException();

            var owners = _mapper.Map<IEnumerable<Owner>>(request.OwnerCollections);
            foreach (var owner in owners)
            {
                _repositoryManager.Owner.CreateOwner(owner);
            }

            await _repositoryManager.SaveAsync();

            var ownerCollection = _mapper.Map<IEnumerable<OwnerDto>>(owners);

            var ownerIds = string.Join(",", ownerCollection.Select(oc => oc.Id));

            return (owners : ownerCollection, ownerIds : ownerIds);
        }
    }
}