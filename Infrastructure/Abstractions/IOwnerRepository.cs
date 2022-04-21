using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Abstractions
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwners(bool trackChanges);
    }
}