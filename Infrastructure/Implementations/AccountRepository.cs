using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Account>> GetOwnerAccounts(Guid ownerId, bool trackChanges)
        {
            return await FindByCondition(a => a.OwnerId.Equals(ownerId), trackChanges).OrderBy(a => a.Name).ToListAsync();
        }
    }
}