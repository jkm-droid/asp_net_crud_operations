using System;

namespace Shared.DataTransferObjects
{
    public record AccountDto
    (
        Guid Id, 
        string Name, 
        string AccountType, 
        DateTime CreatedAt
    );
}