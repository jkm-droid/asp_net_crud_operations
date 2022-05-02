using System;

namespace Domain.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(Guid accountId) : base($"The account with id: {accountId} does not exist in the db")
        { }
    }
}