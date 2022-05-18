using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class ValidateMediaTypeAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var acceptPresentHeader = context.HttpContext.Request.Headers.ContainsKey("Accept");

            if (!acceptPresentHeader)
            {
                context.Result = new BadRequestObjectResult($"Accept header is missing");
                return;
            }

            var mediType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

            if (!MediaTypeHeaderValue.TryParse(mediType, out MediaTypeHeaderValue? outMediaTypeHeaderValue))
            {
                context.Result =
                    new BadRequestObjectResult(
                        $"Media type is not present. Please add Accept header with the required media type");
                return;
            }
            
            context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaTypeHeaderValue);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}