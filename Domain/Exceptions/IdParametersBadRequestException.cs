namespace Domain.Exceptions
{
    public sealed class IdParametersBadRequestException : BadRequestException
    {
        public IdParametersBadRequestException() : base("Ids are null")
        {
        }
    }
}