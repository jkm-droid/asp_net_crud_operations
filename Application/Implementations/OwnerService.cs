using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public OwnerService(IRepositoryManager repository, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repository;
            _loggerManager = loggerManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OwnerDto>> GetAllOwners(bool trackChanges)
        {
            try
            {
                var owners = await _repository.Owner.GetAllOwners(trackChanges);
                var ownersDto = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                
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