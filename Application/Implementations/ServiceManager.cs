using System;
using Application.Abstractions;
using AutoMapper;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;

namespace Application.Implementations
{
    public sealed class ServiceManager : IServiceManager
    {
       public Lazy<IOwnerService> _ownerService;
        public Lazy<IAccountService> _accountService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _ownerService = new Lazy<IOwnerService>(() => new OwnerService(repositoryManager, loggerManager, mapper));
            _accountService = new Lazy<IAccountService>(()=>new AccountService(repositoryManager, loggerManager));
        }

        public IOwnerService OwnerService => _ownerService.Value;

        public IAccountService AccountService => _accountService.Value;

    }
}