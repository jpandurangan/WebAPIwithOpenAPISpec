using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenApiWeb.Controllers
{
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]      //TODO: specify that this action is deprecated. Deprecated does not mean that it is unavailable.
    [ApiVersion("2.0")]
    [Route("{version:apiVersion}/[controller]")]    //TODO: version in URL path
    public class TestVersionController : ControllerBase
    {
        // GET: api/<TestVersionController>
        [HttpGet]
        [MapToApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1" };
        }

        [HttpGet]
        [MapToApiVersion("2.0")]    //TODO: if the controller has multiple versions, specify which version that this action belongs to
        public IEnumerable<string> GetV2()
        {
            return new string[] { "value2" };
        }

        // GET api/<TestVersionController>/5
        [HttpGet("{id}")]  
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestVersionController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestVersionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestVersionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
