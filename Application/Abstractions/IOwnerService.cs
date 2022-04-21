using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAllOwnersAsync(bool trackChanges);
    }
}