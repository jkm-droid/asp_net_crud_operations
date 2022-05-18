using System;

namespace Shared.DataTransferObjects
{
    // [Serializable]
    public record OwnerDto
    {
        public Guid Id { get; init; }
        public string? FullName { get; init; }
        public string? Email { get; init; }
        public string? FullAddress { get; init; }
    }

}