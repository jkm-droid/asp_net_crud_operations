using System;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Abstractions;
using Shared.DataTransferObjects;

namespace Application.Features.Shared
{
    public static class GetAccountOwner
    {
        public static async Task CheckIfAccountOwnerExists(IRepositoryManager repository, Guid ownerId,
            bool trackChanges)
        {
            var owner = await repository.Owner.GetOwnerById(ownerId, trackChanges);
            if (owner is null)
                throw new OwnerNotFoundException(ownerId);
        }
        
        public static async Task<Account> GetAccountAndCheckIfExists(IRepositoryManager repository, Guid ownerId,Guid accountId,
            bool trackChanges)
        {
            var account = await repository.Account.GetAccountById(ownerId,accountId, trackChanges);
            if (account is null)
                throw new OwnerNotFoundException(ownerId);

            return account;
        }
    }
}