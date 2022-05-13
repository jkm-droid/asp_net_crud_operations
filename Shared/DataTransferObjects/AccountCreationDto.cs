using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record AccountCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; init; }
        
        [Required(ErrorMessage = "Account Type is required")]
        public string? AccountType { get; init; }
    }
}