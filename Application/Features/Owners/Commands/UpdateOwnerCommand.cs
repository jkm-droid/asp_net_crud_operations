using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Commands
{
    public class UpdateOwnerCommand : IRequest<Unit>
    {
        public UpdateOwnerCommand(Guid ownerId, OwnerUpdateDto ownerUpdateDto, bool trackChanges)
        {
            OwnerId = ownerId;
            OwnerUpdateDto = ownerUpdateDto;
            TrackChanges = trackChanges;
        }

        public Guid OwnerId { get; set; }
        public OwnerUpdateDto OwnerUpdateDto { get; set; }
        public bool TrackChanges { get; set; }
    }

    internal sealed class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateOwnerCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await _repositoryManager.Owner.GetOwnerById(request.OwnerId, request.TrackChanges);
            if (owner is null)
                throw new OwnerNotFoundException(request.OwnerId);

            _mapper.Map(request.OwnerUpdateDto, owner);
            await _repositoryManager.SaveAsync();
            
            return Unit.Value;
        }
    }
}