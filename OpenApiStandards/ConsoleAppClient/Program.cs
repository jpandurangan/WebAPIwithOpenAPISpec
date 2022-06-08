// See https://aka.ms/new-console-template for more information

using OpenApiGen;

Console.WriteLine("Hello, New World!");

using var httpClient = new HttpClient();
var apiGenClient= new OpenApiGenClient("https://localhost:44384/", httpClient);
var weatherList = await apiGenClient.GetByCityAsync("St. Louis", "1");

foreach (var weather in weatherList)
{
    Console.WriteLine($"City:{weather.City} Date:{weather.Date} Temp:{weather.TemperatureC} Summary:{weather.Summary}");
}

var values = await apiGenClient.ValuesAllAsync("1");
foreach (var intValue in values)
{
    Console.WriteLine(intValue);
}

Console.ReadLine();