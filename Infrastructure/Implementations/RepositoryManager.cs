using System;
using Infrastructure.Abstractions;

namespace Infrastructure.Implementations
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IOwnerRepository> _ownerRepository;
        private readonly Lazy<IAccountRepository> _accountRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _ownerRepository = new Lazy<IOwnerRepository>(() => new OwnerRepository(repositoryContext));
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(repositoryContext));
        }

        public IOwnerRepository Owner => _ownerRepository.Value;

        public IAccountRepository Account => _accountRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();

    }
}