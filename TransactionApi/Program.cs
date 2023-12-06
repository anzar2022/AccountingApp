using Microsoft.EntityFrameworkCore;
using TransactionApi.Data;
using TransactionApi.Dtos;
using TransactionApi.Entities;
using TransactionApi.Repositories;
using TransactionApi.Services;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var dockerConnectionString = configuration.GetConnectionString("DockerConnection");


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = environment == "Production" ? dockerConnectionString : defaultConnectionString;


builder.Services.AddControllers();
builder.Services.AddDbContext<AccountTransactionDBContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();
builder.Services.AddScoped<IAccountTransactionService, AccountTransactionService>();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<AccountTransaction, GetAccountTransactionDto>().ReverseMap();
    config.CreateMap<AccountTransaction, CreateAccountTransactionDto>().ReverseMap();
    config.CreateMap<AccountTransaction, UpdateAccountTransactionDto>().ReverseMap();
  
});

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();
