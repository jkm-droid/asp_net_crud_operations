using Application.Abstractions;
using Infrastructure.Abstractions;
using LoggerService.Abstractions;

namespace Application.Implementations
{
    internal sealed class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _loggerManager;

        public AccountService(IRepositoryManager repository, ILoggerManager loggerManager)
        {
            _repository = repository;
            _loggerManager = loggerManager;
        }
    }
}