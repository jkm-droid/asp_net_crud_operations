using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.RequestFeatures;

namespace Infrastructure.Abstractions
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccounts(bool trackChanges);
        Task<PagedList<Account>> GetOwnerAccounts(Guid ownerId, AccountParameters accountParameters, bool trackChanges);
        Task<Account> GetAccountById(Guid ownerId, Guid accountId, bool trackChanges);
        void CreateAccount(Guid ownerId, Account account);
        
        void DeleteAccount(Account account);
    }
}