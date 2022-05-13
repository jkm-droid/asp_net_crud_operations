using System;
using System.Collections.Generic;
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
    public class GetOwnerAccountsQuery : IRequest<IEnumerable<AccountDto>>
    {
        public Guid OwnerId { get; set; }
        public bool TrackChanges { get; set; }

        public GetOwnerAccountsQuery(Guid ownerd, bool trackChanges)
        {
            OwnerId = ownerd;
            TrackChanges = trackChanges;
        }
    }

    internal sealed class GetOwnerAccountsQueryHandler : IRequestHandler<GetOwnerAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetOwnerAccountsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetOwnerAccountsQuery request, CancellationToken cancellationToken)
        {
            //check if owner exists
            await GetAccountOwner.CheckIfAccountOwnerExists(_repositoryManager,request.OwnerId, trackChanges: false);
            
            var ownerAccounts = await _repositoryManager.Account.GetOwnerAccounts(request.OwnerId, request.TrackChanges);

            var ownerAccountsDto = _mapper.Map<IEnumerable<AccountDto>>(ownerAccounts);

            return ownerAccountsDto;
        }
    }
}