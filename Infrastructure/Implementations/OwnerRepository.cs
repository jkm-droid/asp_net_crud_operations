using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }
        
        public async Task<IEnumerable<Owner>> GetAllOwners(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(o => o.Name).ToListAsync();
        }
    }
}