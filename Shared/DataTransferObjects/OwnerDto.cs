using System;

namespace Shared.DataTransferObjects
{
    public record OwnerDto(Guid Id, string FullName, string Email, string FullAddress);

}