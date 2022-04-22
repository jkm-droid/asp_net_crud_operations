using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccounts(bool trackChanges);
    }
}