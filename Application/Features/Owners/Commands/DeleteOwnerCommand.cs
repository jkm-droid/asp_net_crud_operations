using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.Features.Owners.Commands
{
    public class DeleteOwnerCommand : IRequest<Unit>
    {
        public DeleteOwnerCommand(Guid ownerId, bool trackChanges)
        {
            OwnerId = ownerId;
            TrackChanges = trackChanges;
        }

        public Guid OwnerId { get; set; }
        public bool TrackChanges { get; set; }
    }

    internal sealed class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
    {
        private readonly IRepositoryManager _repository;

        public DeleteOwnerCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await _repository.Owner.GetOwnerById(request.OwnerId, request.TrackChanges);
            if (owner is null)
                throw new OwnerNotFoundException(request.OwnerId);

            _repository.Owner.DeleteOwner(owner);
            await _repository.SaveAsync();
             
            return Unit.Value;
        }
    }
}