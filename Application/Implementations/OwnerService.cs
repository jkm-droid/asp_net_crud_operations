using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;
using Shared.DataTransferObjects;

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

        public async Task<IEnumerable<OwnerDto>> GetAllOwners(bool trackChanges)
        {
            try
            {
                var owners = await _repository.Owner.GetAllOwners(trackChanges);
                var ownersDto = owners.Select(o => new OwnerDto(o.Id, o.Name ?? "", o.Email ?? "", o.Address ?? "", o.Country ?? "")).ToList();
                
                return ownersDto;
            }
            catch (Exception e)
            {
                _loggerManager.LogError($"something occurred {nameof(GetAllOwners)} service method {e}");
                throw;
            }
        }
    }
}