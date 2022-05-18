using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;

namespace Domain.LinkModels
{
    public record LinkParameters(AccountParameters AccountParameters, HttpContext HttpContext);
}