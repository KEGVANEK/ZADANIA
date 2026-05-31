var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var windDirections = new[]
{
    "północny",
    "południowy",
    "wschodni",
    "zachodni",
    "północno-wschodni",
    "północno-zachodni",
    "południowo-wschodni",
    "południowo-zachodni"
};

app.MapGet("/temperature", () =>
{
    var temperature = Random.Shared.Next(-20, 41);
    return Results.Ok(new
    {
        temperature,
        unit = "C"
    });
});

app.MapGet("/wind", () =>
{
    var direction = windDirections[Random.Shared.Next(windDirections.Length)];
    return Results.Ok(new
    {
        direction
    });
});

app.Run();