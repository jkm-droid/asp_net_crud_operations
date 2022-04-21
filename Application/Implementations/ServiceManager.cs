using System;
using Application.Abstractions;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;

namespace Application.Implementations
{
    public sealed class ServiceManager : IServiceManager
    {
        public readonly IRepositoryManager _repositoryWrapper;
        private readonly ILoggerManager _loggerManager;
        public Lazy<IOwnerService> _ownerService;
        public Lazy<IAccountService> _accountService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
        {
            _ownerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager, loggerManager));
            _accountService = new Lazy<IAccountService>(()=>new AccountService(repositoryManager, loggerManager));
        }

        public IOwnerService OwnerService => _ownerService.Value;

        public IAccountService AccountService => _accountService.Value;

    }
}