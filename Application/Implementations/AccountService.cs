using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
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

        public async Task<IEnumerable<Account>> GetAllAccounts(bool trackChanges)
        {
            try
            {
                var accounts = await _repository.Account.GetAllAccounts(trackChanges: false);

                return accounts;
            }
            catch (Exception e)
            {
                _loggerManager.LogError($"something occurred {nameof(GetAllAccounts)} service method {e}");
                throw;
            }
        }
    }
}