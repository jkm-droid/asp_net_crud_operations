using System;

namespace Domain.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        protected AccountNotFoundException(Guid accountId) : base($"The account with id: {accountId} does not exist in the db")
        { }
    }
}