namespace OpenApiWeb
{
    public class WeatherForecastFox: WeatherForecast
    {
        public override string Summary { get => base.Summary; set => base.Summary = value + "Provided by Fox"; }
    }
}
