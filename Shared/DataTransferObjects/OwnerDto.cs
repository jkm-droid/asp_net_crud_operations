using System;

namespace Shared.DataTransferObjects
{
    public record OwnerDto(Guid Id, string Name, string Email, string Address, string Country);

}