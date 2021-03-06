using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Shared;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Accounts.Queries
{
    public class GetAccountForPatchQuery : IRequest<(AccountUpdateDto accountPatch, Account account)>
    {
        public GetAccountForPatchQuery( Guid ownerId, Guid accountId, bool ownerTrackChanges, bool accountTrackChanges)
        {
            OwnerId = ownerId;
            AccountId = accountId;
            OwnerTrackChanges = ownerTrackChanges;
            AccountTrackChanges = accountTrackChanges;
        }
        public Guid OwnerId { get; set; }
        public Guid AccountId { get; set; }
        public bool OwnerTrackChanges { get; set; }
        public bool AccountTrackChanges { get; set; }
    }

    internal sealed class GetAccountForPatchQueryHandler : IRequestHandler<GetAccountForPatchQuery, (AccountUpdateDto
        accountPatch, Account account)>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetAccountForPatchQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<(AccountUpdateDto accountPatch, Account account)> Handle(GetAccountForPatchQuery request, CancellationToken cancellationToken)
        {
            await GetAccountOwner.CheckIfAccountOwnerExists(_repositoryManager,request.OwnerId, trackChanges: false);

            var accountEntity =await GetAccountOwner.GetAccountAndCheckIfExists(_repositoryManager, request.OwnerId,
                request.AccountId, request.AccountTrackChanges);

            var accountPatch = _mapper.Map<AccountUpdateDto>(accountEntity);

            return (accountPatch, accountEntity);
        }
    }
}