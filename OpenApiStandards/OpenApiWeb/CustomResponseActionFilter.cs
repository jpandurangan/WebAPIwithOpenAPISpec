using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenApiWeb
{
    public class CustomResponseActionFilter : ActionFilterAttribute
    {
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base
        //}

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var httpStatusCode = GetStatusCode(context);
            if (httpStatusCode.HasValue && httpStatusCode >= 400 && httpStatusCode <= 500)
            {
                
            }

            base.OnActionExecuted(context);
        }

        private static int? GetStatusCode(ActionExecutedContext context)
        {
            ObjectResult result = context.Result as ObjectResult;
            var statusCode = result?.StatusCode;

            if (statusCode == null)
            {
                StatusCodeResult statusResult = context.Result as StatusCodeResult;
                statusCode = statusResult?.StatusCode;
            }

            return statusCode;
        }
    }
}
