using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]  //TODO: change the API version to 0 to demo that when the entire version APIs are deprecated, Swagger would mark it as deprecated
    public class BetaController : ControllerBase
    {
        // GET: api/<BetaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BetaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BetaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BetaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BetaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
