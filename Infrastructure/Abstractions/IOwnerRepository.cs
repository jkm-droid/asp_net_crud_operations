using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Abstractions
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllOwners(bool trackChanges);
        Task<Owner> GetOwnerById(Guid ownerId, bool trackChanges);
        void CreateOwner(Owner owner);
        Task<IEnumerable<Owner>> GetOwnersByIds(IEnumerable<Guid> ownerIds, bool trackChanges);
    }
}