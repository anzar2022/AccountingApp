using AccountDBUtilities.Data;
using AccountDBUtilities.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var dockerConnectionString = configuration.GetConnectionString("DockerConnection");

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = environment == "Production" ? dockerConnectionString : defaultConnectionString;

builder.Services.AddDbContext<AccountingAppDBContext>(options => options.UseSqlServer(connectionString));




var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
