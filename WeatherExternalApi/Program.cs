using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var apiKey = "74d3bab7c6cf772b6771aeaba2425b16";

app.MapGet("/weather/{city}", async (string city, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient();

    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=pl";

    var response = await client.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
        return Results.BadRequest("Nie udało się pobrać pogody. Sprawdź nazwę miasta albo klucz API.");
    }

    var json = await response.Content.ReadAsStringAsync();
    var data = JsonDocument.Parse(json);

    var root = data.RootElement;

    var cityName = root.GetProperty("name").GetString();
    var temperature = root.GetProperty("main").GetProperty("temp").GetDouble();
    var feelsLike = root.GetProperty("main").GetProperty("feels_like").GetDouble();
    var humidity = root.GetProperty("main").GetProperty("humidity").GetInt32();
    var windSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble();
    var description = root.GetProperty("weather")[0].GetProperty("description").GetString();

    return Results.Ok(new
    {
        city = cityName,
        temperature = temperature,
        feelsLike = feelsLike,
        humidity = humidity,
        windSpeed = windSpeed,
        description = description
    });
})
.WithName("GetWeatherByCity")
.WithOpenApi();

app.Run();