namespace Domain.Exceptions
{
    public class OwnerCollectionBadRequest : BadRequestException
    {
        public OwnerCollectionBadRequest() : base("Owner collection is null")
        {
        }
    }
}