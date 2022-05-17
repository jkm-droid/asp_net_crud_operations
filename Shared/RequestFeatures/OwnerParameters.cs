namespace Shared.RequestFeatures
{
    public class OwnerParameters : RequestParameters
    {
        public OwnerParameters()
        {
            OrderBy = "name";
        }
        public string? SearchTerm { get; set; }
    }
}