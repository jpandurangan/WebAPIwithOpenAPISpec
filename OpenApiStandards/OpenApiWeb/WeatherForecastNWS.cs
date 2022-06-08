using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OpenApiWeb;

namespace NationalWeatherService
{
    public class WeatherForecast
    {
        /// <summary>
        /// Weather Forecast Date
        /// </summary>
        /// <example>12/12/2021</example>
        /// <format>date</format>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(CustomJsonDateConvert))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature in Celcius
        /// </summary>
        /// <example>30</example>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperature in Fahrenheit
        /// </summary>
        /// <example>-1</example>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Description of weather forecast
        /// </summary>
        /// <example>Mostly sunny</example>
        public virtual string Summary { get; set; }

        /// <summary>
        /// The name of the city
        /// </summary>
        /// <example>St. Louis</example>
        [Required]
        [StringLength(30)]
        public string City { get; set; }

        
        public Forecast ForecastAccuracy { get;set; }

        [Obsolete]
        public string Meteorologist { get; set; }
    }

    /// <summary>
    /// Accuracy of weather forecast prediction
    /// </summary>
    /// <example>ACCURATE</example>
    public enum Forecast
    {
        RANDOM,
        PROPABLE,
        ACCURATE
    }
}
