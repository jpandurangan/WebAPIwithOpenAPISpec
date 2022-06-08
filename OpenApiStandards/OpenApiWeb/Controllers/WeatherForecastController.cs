using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OpenApiWeb.Controllers
{
    ///<summary>
    ///Get weather details using this API
    ///</summary>
    [ApiController]
    [ApiVersion("1.0")] //TODO: version in URL querystring
    [Route("[controller]")]     
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets forecast for all the cities
        /// </summary>
        /// <param name="city">City where you want the forecast for</param>
        /// <returns>Weather for a given city</returns>
        /// <response code="200">Returns the weather for the city</response>
        /// <response code="404">If the city not matched</response>
        [HttpGet("{city}", Name = nameof(GetByCity))]   //TODO: Name property is used as operationId in the spec json
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<WeatherForecast> GetByCity(string city)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = city+ index
            })
            .ToArray();
        }

        [Obsolete]
        [HttpGet("obsolete", Name = nameof(GetByCityObsolete))]
        public string GetByCityObsolete(string city)
        {
            return "This method is obsolete";
        }

        [ApiVersion("2.0")] //TODO: override particular action's version from the controller's version
        [HttpGet("{code}", Name = nameof(GetByCode))]
        public IEnumerable<WeatherForecast> GetByCode(string code)
        {
            return new List<WeatherForecast>();
        }

        [HttpGet("{city}/{zip}", Name = nameof(GetByCityZip))]
        public IActionResult GetByCityZip([Required]string city, string zip) //TODO: add data annotation to param to validate
        {
            //TODO: warning showing that ProducesResponseType is missing. Warning is enabled by adding Analyzer to the proj file.
            
            var problemDetails = this.BuildProblemDetails(HttpStatusCode.NotFound,$"Cannot find {city} and {zip} combination");
            return NotFound(problemDetails);    
        }

        [HttpGet("{city}/extended", Name = nameof(GetByCityExtended))]
        public WeatherForecastExtended GetByCityExtended([Required] string city) 
        {
            return new WeatherForecastExtended { Next7Days = "Humid" };
        }

        [HttpGet("{city}/provider/{provider}", Name = nameof(GetByCityAndProvider))]
        public WeatherForecast GetByCityAndProvider([Required] string city, string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                provider = "NBC";
            }

            if(provider == "NBC")
            {
                return new WeatherForecastNBC();
            }
            return new WeatherForecastFox();
        }

        //[HttpGet("NWS")]
        //public NationalWeatherService.WeatherForecast GetWeatherForecastNWS()
        //{
        //    return new NationalWeatherService.WeatherForecast();
        //}

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)] //TODO: do not expose this method in swagger & json
        [Route("month")]
        public IActionResult Get(int month)
        {
            return Content("test");  
        }

        [NonAction] //TODO: mark public methods that are not actions
        public bool Delete(string city)
        {
            return true;
        }
    }
}
