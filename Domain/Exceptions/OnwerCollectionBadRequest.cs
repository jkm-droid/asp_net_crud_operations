namespace Domain.Exceptions
{
    public class OnwerCollectionBadRequest : BadRequestException
    {
        public OnwerCollectionBadRequest() : base("Owner collection is null")
        {
        }
    }
}