using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Application.Abstractions
{
    public interface IOwnerService
    {
        Task<IEnumerable<OwnerDto>> GetAllOwners(bool trackChanges);
    }
}