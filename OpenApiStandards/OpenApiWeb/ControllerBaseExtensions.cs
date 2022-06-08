using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace OpenApiWeb
{
    public static class ControllerBaseExtensions
    {
        public static ProblemDetails BuildProblemDetails(this ControllerBase controllerBase, HttpStatusCode httpStatusCode, string detail, string title = null, IDictionary<string, object> extData = null)
        {
            var problemDetails = new ProblemDetails()
            {
                Detail = detail,
                Title = title ?? ReasonPhrases.GetReasonPhrase((int)httpStatusCode),
                Instance = controllerBase.Request.Path,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
            };

            var traceId = Activity.Current?.Id ?? controllerBase.HttpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            if (extData != null)
            {
                foreach (var keyValuePair in extData)
                {
                    problemDetails.Extensions[keyValuePair.Key] = keyValuePair.Value;
                }
            }
            return problemDetails;
        }

    }
}
