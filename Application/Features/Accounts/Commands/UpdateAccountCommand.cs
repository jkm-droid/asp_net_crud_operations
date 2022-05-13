using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Shared;
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
            await GetAccountOwner.CheckIfAccountOwnerExists(_repositoryManager,request.OwnerId, trackChanges: false);

            var ownerAccount = await GetAccountOwner.GetAccountAndCheckIfExists(_repositoryManager, request.OwnerId,
                request.AccountId, request.AccountTrackChanges);
               
            _mapper.Map(request.AccountUpdateDto, ownerAccount);
            await _repositoryManager.SaveAsync();
            
            return Unit.Value;
        }
    }
}