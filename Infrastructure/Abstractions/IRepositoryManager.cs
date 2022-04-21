
namespace Infrastructure.Abstractions
{
    public interface IRepositoryManager
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        void Save();
    }
}