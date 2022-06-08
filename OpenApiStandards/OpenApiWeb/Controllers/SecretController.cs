using Microsoft.AspNetCore.Mvc;

namespace OpenApiWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)] //TODO: hides this API being exposed in any API explorer (like swagger). In general avoid hiding by obscuring
    public class SecretController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new{});
        }
    }
}
