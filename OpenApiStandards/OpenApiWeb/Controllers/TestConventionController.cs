using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace OpenApiWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class TestConventionController : ControllerBase
    {
        [HttpGet]
        [ApiConventionMethod(typeof(KeefeGetApiConventions),nameof(KeefeGetApiConventions.Get))]    //TODO: applies custom defined API standards to the http verbs
        public IEnumerable<int> Get()
        {
            return Enumerable.Range(1, 5).Select(x => x);
        }

    }
}
