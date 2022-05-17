namespace Domain.Exceptions
{
    public class ValidDateRangeBadRequestException : BadRequestException
    {
        public ValidDateRangeBadRequestException() : base("Invalid Date range")
        {
        }
    }
}