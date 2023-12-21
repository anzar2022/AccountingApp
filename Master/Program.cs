using AccountDatabase.Data;
using AccountDatabase.Entities;
using Master.Repositories;
using Master.Services;
using Microsoft.EntityFrameworkCore;
using static Master.Dtos.MasterDto;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var dockerConnectionString = configuration.GetConnectionString("DockerConnection");

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "" ? "local" : Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var connectionString = environment == "Production" ? dockerConnectionString : defaultConnectionString;

Console.WriteLine("Connction string {0} :", connectionString);
Console.WriteLine("environment {0} : ", environment);
builder.Services.AddControllers();
builder.Services.AddDbContext<AccountingAppDBContext>(options =>
{
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("AccountApi"));

});

builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<IMasterService, MasterService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<InterestMaster, GetInterestDto>().ReverseMap();
    config.CreateMap<InterestMaster, CreateInterestDto>().ReverseMap();
    config.CreateMap<InterestMaster, UpdateInterestDto>().ReverseMap();


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
app.UseCors("EnableCORS");

app.MapGet("/", () => "Hello World!");

app.Run();