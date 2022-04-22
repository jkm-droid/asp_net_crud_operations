namespace Shared.DataTransferObjects
{
    public record OwnerCreationDto
    (
        string Name,
        string Email, 
        string Address,
        string Country
    );
}