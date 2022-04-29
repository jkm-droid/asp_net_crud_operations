using System.Collections.Generic;

namespace Shared.DataTransferObjects
{
    public record OwnerCreationDto
    (
        string Name,
        string Email, 
        string Address,
        string Country,
        IEnumerable<AccountCreationDto> Accounts//will enable creating an owner together with an account
    );
}