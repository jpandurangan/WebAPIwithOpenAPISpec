namespace OpenApiWeb
{
    public class WeatherForecastNBC: WeatherForecast
    {
        public override string Summary { get => base.Summary; set => base.Summary = value + "Provided by NBC"; }
    }
}
