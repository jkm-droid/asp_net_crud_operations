namespace Infrastructure.Abstractions
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get;}
        IAccountRepository Account { get;}
        void Save();
    }
}