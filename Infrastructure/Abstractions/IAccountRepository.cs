using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Abstractions
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccounts(bool trackChanges);
    }
}