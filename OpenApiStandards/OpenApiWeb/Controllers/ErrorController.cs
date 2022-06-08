using System;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace OpenApiWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            
            var problem = Problem(
                detail: context.Error.StackTrace,
                instance: HttpContext.Features.Get<IHttpRequestFeature>().RawTarget);

            AddDetailsToResponse(context.Error, (ProblemDetails)problem.Value);
            
            return problem;
        }

        private static void AddDetailsToResponse(Exception ex, ProblemDetails problemDetails)
        {
            var errorMessage = ex.Message;
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                var index = errorMessage.LastIndexOf("{\"ExtraInfo\"", StringComparison.Ordinal);
                if (index != -1) 
                {
                    var extraDetails = errorMessage.Substring(index);
                    var errorAdditionalDetails = JsonSerializer.Deserialize<ErrorAdditionalDetails>(extraDetails);
                    if (errorAdditionalDetails != null)
                    {
                        foreach (var keyValuePair in errorAdditionalDetails.ExtraInfo)
                        {
                            problemDetails.Extensions.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }

                    problemDetails.Title = errorMessage.Substring(0, index).Trim();
                }
                else
                {
                    problemDetails.Title = errorMessage;
                }
            }

            if (ex.InnerException != null)
            {
                problemDetails.Extensions.Add("innerException", ex.InnerException);
            }

            

        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
