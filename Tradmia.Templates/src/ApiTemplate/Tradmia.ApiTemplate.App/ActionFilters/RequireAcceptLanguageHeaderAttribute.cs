using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tradmia.ApiTemplate.App.ActionFilters;

public class RequireAcceptLanguageHeaderAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var headers = context.HttpContext.Request.Headers;

        if (!headers.ContainsKey("Accept-Language"))
        {
            context.Result = new ContentResult
            {
                Content = "Missing Accept-Language header",
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        base.OnActionExecuting(context);
    }
}
