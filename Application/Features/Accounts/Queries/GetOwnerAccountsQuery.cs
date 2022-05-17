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
using Shared.RequestFeatures;

namespace Application.Features.Accounts.Queries
{
    public class GetOwnerAccountsQuery : IRequest<(IEnumerable<AccountDto> accounts, PagingMetaData pagingMetaData)>
    {
        public Guid OwnerId { get; set; }
        public bool TrackChanges { get; set; }
        
        public AccountParameters AccountParameters { get; set; }

        public GetOwnerAccountsQuery(Guid ownerId,AccountParameters accountParameters, bool trackChanges)
        {
            OwnerId = ownerId;
            TrackChanges = trackChanges;
            AccountParameters = accountParameters;
        }
    }

    internal sealed class GetOwnerAccountsQueryHandler : IRequestHandler<GetOwnerAccountsQuery, (IEnumerable<AccountDto> accounts, PagingMetaData pagingMetaData)>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetOwnerAccountsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<AccountDto> accounts, PagingMetaData pagingMetaData)> Handle(GetOwnerAccountsQuery request, CancellationToken cancellationToken)
        {
            if (!request.AccountParameters.ValidDateRange)
                throw new ValidDateRangeBadRequestException();
                //check if owner exists
            await GetAccountOwner.CheckIfAccountOwnerExists(_repositoryManager,request.OwnerId, trackChanges: false);
            
            var ownerAccounts = await _repositoryManager.Account.GetOwnerAccounts(request.OwnerId, request.AccountParameters, request.TrackChanges);

            var ownerAccountsDto = _mapper.Map<IEnumerable<AccountDto>>(ownerAccounts);

            return (accounts: ownerAccountsDto, pagingMetaData: ownerAccounts.PagingMetaData);
        }
    }
}