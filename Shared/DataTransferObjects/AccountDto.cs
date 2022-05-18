using System;

namespace Shared.DataTransferObjects
{
    public record AccountDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? AccountType { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}