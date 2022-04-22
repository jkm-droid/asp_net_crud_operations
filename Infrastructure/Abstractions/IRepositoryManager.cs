
using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public interface IRepositoryManager
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        Task SaveAsync();
    }
}