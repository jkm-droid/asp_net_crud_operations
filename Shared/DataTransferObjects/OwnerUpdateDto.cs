using System;
using System.Collections.Generic;

namespace Shared.DataTransferObjects
{
    public record OwnerUpdateDto
    (
        string Name,
        string Email, 
        string Address,
        string Country,
        IEnumerable<AccountCreationDto> Accounts//will enable creating an account while updating owner
    );

}