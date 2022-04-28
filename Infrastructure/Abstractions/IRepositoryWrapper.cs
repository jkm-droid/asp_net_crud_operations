using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get;}
        IAccountRepository Account { get;}
        Task SaveAsync();
    }
}