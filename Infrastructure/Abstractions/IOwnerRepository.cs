using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.RequestFeatures;

namespace Infrastructure.Abstractions
{
    public interface IOwnerRepository
    {
        Task<PagedList<Owner>> GetAllOwners(OwnerParameters ownerParameters,bool trackChanges);
        Task<Owner> GetOwnerById(Guid ownerId, bool trackChanges);
        void CreateOwner(Owner owner);
        Task<IEnumerable<Owner>> GetOwnersByIds(IEnumerable<Guid> ownerIds, bool trackChanges);
        void DeleteOwner(Owner owner);
    }
}