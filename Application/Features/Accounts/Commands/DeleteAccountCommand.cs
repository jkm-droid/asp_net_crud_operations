using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.Features.Accounts.Commands
{
    public class DeleteAccountCommand : IRequest<Unit>
    {
        public DeleteAccountCommand(Guid ownerId,Guid accountId,  bool trackChanges)
        {
            TrackChanges = trackChanges;
            AccountId = accountId;
            OwnerId = ownerId;
        }

        public Guid OwnerId { get; set; }
        public Guid AccountId { get; set; }
        public bool TrackChanges { get; set; }
        
    }

    internal sealed class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IRepositoryManager _repository;

        public DeleteAccountCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            // await _getOwner.GetAccountOwnerAndCheckIfExists(_repository,request.OwnerId, trackChanges: false);

            var ownerAccount =
                await _repository.Account.GetAccountById(request.OwnerId, request.AccountId, request.TrackChanges);
            if (ownerAccount is null)
                throw new AccountNotFoundException(request.AccountId);
            
            _repository.Account.DeleteAccount(ownerAccount);
            await _repository.SaveAsync();
            
            return Unit.Value;
        }
    }
}