using AccountDatabase.Data;
using AccountDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using TransactionApi.Clients;
using TransactionApi.Data;
using TransactionApi.Dtos;
using TransactionApi.Repositories;
using TransactionApi.Services;
using static TransactionApi.Dtos.InterestEMIDto;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
var dockerConnectionString = configuration.GetConnectionString("DockerConnection");


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var connectionString = environment == "Production" ? dockerConnectionString : defaultConnectionString;


builder.Services.AddControllers();
builder.Services.AddDbContext<AccountingAppDBContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();
builder.Services.AddScoped<IAccountTransactionService, AccountTransactionService>();
builder.Services.AddScoped<IInterestTransactionRepository, InterestTransactionRepository>();
builder.Services.AddScoped<IInterestTransactionService, InterestTransactionService>();



builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<AccountTransaction, GetAccountTransactionDto>().ReverseMap();
    config.CreateMap<AccountTransaction, CreateAccountTransactionDto>().ReverseMap();
    config.CreateMap<AccountTransaction, UpdateAccountTransactionDto>().ReverseMap();
    config.CreateMap<InterestEMI, GetInterestEMIDto>().ReverseMap();
  
});

builder.Services.AddHttpClient<AccountClient>(client => {
    client.BaseAddress = new Uri("http://localhost:5289/"); // Replace "https://example.com" with your actual base URL
});

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
app.MapGet("/", () => "Hello World!");
app.UseCors("EnableCORS");
app.Run();
