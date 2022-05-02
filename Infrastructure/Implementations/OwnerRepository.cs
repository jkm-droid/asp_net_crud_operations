using System;
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

        public async Task<Owner> GetOwnerById(Guid ownerId, bool trackChanges)
        {
            return await FindByCondition(o => o.Id.Equals(ownerId), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateOwner(Owner owner)
        {
          Create(owner);
        }
        
        public async Task<IEnumerable<Owner>> GetOwnersByIds(IEnumerable<Guid> ownerIds, bool trackChanges)
        {
            return await FindByCondition(o => ownerIds.Contains(o.Id), trackChanges).ToListAsync();
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }
    }
}