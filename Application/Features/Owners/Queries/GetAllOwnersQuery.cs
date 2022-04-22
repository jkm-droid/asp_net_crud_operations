using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;
using MediatR;
using Shared.DataTransferObjects;

namespace Application.Features.Owners.Queries
{
    public class GetAllOwnersQuery : IRequest<IEnumerable<OwnerDto>>
    {
        public GetAllOwnersQuery()
        {
            
        }
    }
    
    internal sealed class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, IEnumerable<OwnerDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public GetAllOwnersQueryHandler(IRepositoryManager repository, ILoggerManager loggerManager)
        {
            _repository = repository;
            _loggerManager = loggerManager;
        }

        public async Task<IEnumerable<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var owners = await _repository.Owner.GetAllOwners(trackChanges: false);
                var ownersDto = owners.Select(o => new OwnerDto(o.Id, o.Name ?? "", o.Email ?? "", o.Address ?? "", o.Country ?? "")).ToList();
                
                return ownersDto;
            }
            catch (Exception e)
            {
                _loggerManager.LogError($"something occurred service method {e}");
                throw;
            }
        }
    }
}