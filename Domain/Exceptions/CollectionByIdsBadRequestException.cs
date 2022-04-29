namespace Domain.Exceptions
{
    public sealed class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException() : base("Id collection count mismatch when comparing ids")
        {
        }
    }
}