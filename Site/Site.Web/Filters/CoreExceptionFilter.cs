using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Site.Core.Conversions;
using Site.Core.Exceptions;

namespace Site.Web.Filters
{
    public class CoreExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Do nothing when the request comes in
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ExceptionBase exceptionBase)
            {
                context.Result = new ObjectResult(exceptionBase.AsDto()) {StatusCode = StatusCodes.Status400BadRequest};
                context.ExceptionHandled = true;
            }
        }

        
    }
}