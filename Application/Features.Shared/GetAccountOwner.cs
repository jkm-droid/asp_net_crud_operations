using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Abstractions;

namespace Application.Features.Shared
{
    public static class GetAccountOwner
    {
        public static async Task GetAccountOwnerAndCheckIfExists(IRepositoryManager repository, Guid ownerId,
            bool trackChanges)
        {
            var owner = await repository.Owner.GetOwnerById(ownerId, trackChanges);
            if (owner is null)
                throw new OwnerNotFoundException(ownerId);
        }
    }
}