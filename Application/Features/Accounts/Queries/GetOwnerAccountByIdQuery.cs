using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Accounts.Queries
{
    public class GetOwnerAccountByIdQuery : IRequest<AccountDto>
    {
        public GetOwnerAccountByIdQuery(Guid ownerId, Guid accountId, bool trackChanges)
        {
            OwnerId = ownerId;
            AccountId = accountId;
            TrackChanges = trackChanges;
        }

        public Guid OwnerId { get; set; }
        public Guid AccountId { get; set; }
        public bool TrackChanges { get; set; }
    }

    internal sealed class GetOwnerAccountByIdQueryHandler : IRequestHandler<GetOwnerAccountByIdQuery, AccountDto>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;

        public GetOwnerAccountByIdQueryHandler(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        public async Task<AccountDto> Handle(GetOwnerAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _repositoryManager.Owner.GetOwnerById(request.OwnerId, trackChanges: false);
            if (owner is null)
                throw new OwnerNotFoundException(request.OwnerId);  

            var account =
                await _repositoryManager.Account.GetAccountById(request.OwnerId, request.AccountId, request.TrackChanges);
            var accountToReturn = _mapper.Map<AccountDto>(account);

            return accountToReturn;
        }
    }
}