using Microsoft.AspNetCore.Mvc;

namespace OpenApiWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class NoVersionController : ControllerBase
    {
        [HttpGet]
        public string Get() => HttpContext.GetRequestedApiVersion().ToString();
    }
}
