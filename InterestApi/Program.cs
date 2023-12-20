using InterestMasterApi.Data;
using InterestMasterApi.Entities;
using InterestMasterApi.Repositories;
using InterestMasterApi.Services;
using Microsoft.EntityFrameworkCore;
using static InterestMasterApi.Dtos.InterestMasterDto;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var dockerConnectionString = configuration.GetConnectionString("DockerConnection");


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = environment == "Production" ? dockerConnectionString : defaultConnectionString;

builder.Services.AddControllers();
builder.Services.AddDbContext<InterestMasterDBContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IInterestMasterService, InterestMasterService>();
builder.Services.AddScoped<IInterestMasterRepository, InterestMasterRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<InterestMaster, GetInterestDto>().ReverseMap();
  


});
var app = builder.Build();

app.MapGet("/", () => "Hello World Interest!");

app.Run();


