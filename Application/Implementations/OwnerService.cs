using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;

namespace Application.Implementations
{
    internal sealed class OwnerService : IOwnerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public OwnerService(IRepositoryManager repository, ILoggerManager loggerManager)
        {
            _repository = repository;
            _loggerManager = loggerManager;
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync(bool trackChanges)
        {
            try
            {
                var owners = await _repository.Owner.GetAllOwners(trackChanges);
                
                return owners;
            }
            catch (Exception e)
            {
                _loggerManager.LogError($"something occurred {nameof(GetAllOwnersAsync)} service method {e}");
                throw;
            }
        }
    }
}