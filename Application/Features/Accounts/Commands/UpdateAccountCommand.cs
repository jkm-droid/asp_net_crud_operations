using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Accounts.Commands
{
    public class UpdateAccountCommand : IRequest<Unit>
    {
        public UpdateAccountCommand(AccountUpdateDto accountUpdateDto, Guid ownerId, Guid accountId, bool ownerTrackChanges, bool accountTrackChanges)
        {
            OwnerId = ownerId;
            AccountId = accountId;
            OwnerTrackChanges = ownerTrackChanges;
            AccountTrackChanges = accountTrackChanges;
            AccountUpdateDto = accountUpdateDto;
        }

        public AccountUpdateDto AccountUpdateDto { get; set; }
        public Guid OwnerId { get; set; }
        public Guid AccountId { get; set; }
        public bool OwnerTrackChanges { get; set; }
        public bool AccountTrackChanges { get; set; }
    }

    internal sealed class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var owner = await _repositoryManager.Owner.GetOwnerById(request.OwnerId, request.OwnerTrackChanges);
            if (owner is null)
                throw new OwnerNotFoundException(request.OwnerId);

            var ownerAccount =
                await _repositoryManager.Account.GetAccountById(request.OwnerId, request.AccountId, request.AccountTrackChanges);
            if (ownerAccount is null)
                throw new AccountNotFoundException(request.AccountId);

            _mapper.Map(request.AccountUpdateDto, ownerAccount);
            await _repositoryManager.SaveAsync();
            
            return Unit.Value;
        }
    }
}