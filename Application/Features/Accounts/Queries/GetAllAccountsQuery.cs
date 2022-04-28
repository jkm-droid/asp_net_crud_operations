using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Accounts.Queries
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<AccountDto>>
    {
        public GetAllAccountsQuery()
        {
            
        }
    }

    internal sealed class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public GetAllAccountsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            
            var accounts = await _repositoryManager.Account.GetAllAccounts(trackChanges: false);
            var accountsDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);

            return accountsDto;
        }
    }
}