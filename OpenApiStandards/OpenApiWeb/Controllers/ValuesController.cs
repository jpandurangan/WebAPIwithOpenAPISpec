using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace OpenApiWeb.Controllers
{
    ///<summary>
    ///Get values using this API. Demo exceptions
    ///</summary>
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]  //TODO: applies Open API standards for the Http verbs
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return Enumerable.Range(1, 5).Select(x => x);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ValuesModel valuesModel)
        {
            switch (valuesModel.Zip)
            {
                case 66666:
                    var additionalDetails = new ErrorAdditionalDetails();   //TODO: adds additional properties to the error response object
                    additionalDetails.ExtraInfo.Add("User","KeefeAdmin");
                    additionalDetails.ExtraInfo.Add("Agency", "St. Louis DOC");
                    throw new InvalidOperationException($"Zip 6666 is not allowed {additionalDetails}");
                case 65555:
                    throw new Exception("Posting failed in DB");
                case 64444:
                    return Problem("Test problem result", "/Values", 403, "Test error", null);
                default:
                    return Created("uri", new { });
            }
        }

    }
}
