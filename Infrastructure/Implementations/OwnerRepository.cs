using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Infrastructure.Implementations
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }
        
        public async Task<PagedList<Owner>> GetAllOwners(OwnerParameters ownerParameters,bool trackChanges)
        {
            var owners =  await FindAll( trackChanges)
                .OrderBy(o => o.Name)
                .Search(ownerParameters.SearchTerm)
                .Sort(ownerParameters.OrderBy)
                .Skip((ownerParameters.PageNumber - 1) * ownerParameters.PageSize)
                .Take(ownerParameters.PageSize)
                .ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<Owner>(owners, count, ownerParameters.PageNumber, ownerParameters.PageSize);
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