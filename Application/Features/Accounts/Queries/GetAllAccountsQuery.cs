using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public GetAllAccountsQueryHandler(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _repositoryManager.Account.GetAllAccounts(trackChanges: false);
                var accountsDto = accounts.Select(a => new AccountDto(a.Id, a.Name ?? "",a.AccountType ?? "", a.CreatedAt)).ToList();

                return accountsDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}