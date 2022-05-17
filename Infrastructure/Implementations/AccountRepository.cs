using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Infrastructure.Implementations
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAccounts(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<PagedList<Account>> GetOwnerAccounts(Guid ownerId, AccountParameters accountParameters, bool trackChanges)
        {
            var accounts = await FindByCondition(a => a.OwnerId.Equals(ownerId) , trackChanges)
                .FilterAccounts(accountParameters.PeriodFrom, accountParameters.PeriodTo)
                .Search(accountParameters.SearchTerm)
                .Sort(accountParameters.OrderBy)
                .Skip((accountParameters.PageNumber - 1) * accountParameters.PageSize)
                .Take(accountParameters.PageSize)
                .OrderBy(a => a.Name).ToListAsync();

            var count = await FindByCondition(a => a.OwnerId.Equals(ownerId), trackChanges).CountAsync();

            return new PagedList<Account>(accounts, count, accountParameters.PageNumber, accountParameters.PageSize);
        }

        public async Task<Account> GetAccountById(Guid ownerId, Guid accountId, bool trackChanges)
        {
            return await FindByCondition(a => a.OwnerId.Equals(ownerId) && a.Id.Equals(accountId), trackChanges)
                .SingleOrDefaultAsync();
        }
        public void CreateAccount(Guid ownerId, Account account)
        {
            account.OwnerId = ownerId;
            account.CreatedAt = DateTime.Now;
            Create(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }
    }
}