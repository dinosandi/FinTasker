using FinTasker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinTasker.Application.Common.Interfaces;
using MediatR;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


builder.Services.AddScoped<IJwtService, JwtService>();

//Root Clean Architecture: Register AppDbContext and IAppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAppDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("FinTasker.Application")));


builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAppDbContext, AppDbContext>();


var googleSection = builder.Configuration.GetSection("Authentication:Google");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "Google";
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = googleSection.GetValue<string>("ClientId")!;
        options.ClientSecret = googleSection.GetValue<string>("ClientSecret")!;
        options.CallbackPath = "/signin-google";
        Console.WriteLine($"ClientId: {googleSection["ClientId"]}");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
