using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record OwnerCreationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Name should be less than 20 characters")]
        public string? Name { get; init; }
        
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }
        
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; init; }
        
        private IEnumerable<AccountCreationDto>? Accounts { get; init; } //will enable creating an owner together with an account
    }
}