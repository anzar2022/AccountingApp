using AccountApi.Data;
using AccountApi.Dtos;
using AccountApi.Repositories;
using AccountApi.Services;
using AccountDBUtilities.Data;
using AccountDBUtilities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var dockerConnectionString = configuration.GetConnectionString("DockerConnection");

Console.WriteLine(defaultConnectionString);
Console.WriteLine(dockerConnectionString);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = environment == "Production" ? dockerConnectionString : defaultConnectionString;

Console.WriteLine(connectionString);
Console.WriteLine(environment);



builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddDbContext<AccountingAppDBContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<AccountingAppDBContext>();


//builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Account, GetAccountDto>().ReverseMap();
    config.CreateMap<Account, CreateAccountDto>().ReverseMap();
    config.CreateMap<Account, UpdateAccountDto>().ReverseMap();
   
}, typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

var app = builder.Build();
app.MapControllers();


// Configure the HTTP request pipeline.

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
});
app.UseCors("EnableCORS");
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
