using Application.Data.Employees;
using Microsoft.EntityFrameworkCore;
using Tradmia.ApiTemplate.App.Middlewares;
using Tradmia.ApiTemplate.Application;
using Tradmia.ApiTemplate.Application.Caching;
using Tradmia.ApiTemplate.Application.Data.Employees;
using Tradmia.ApiTemplate.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<AppDbContext>();

builder.Services.AddScoped<IEmployeeAppService, EmployeeAppService>();

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration.GetConnectionString("Redis");
    redisOptions.Configuration = connection;
});
builder.Services.AddSingleton<ICacheService, CacheService>();


string EndpointUri = builder.Configuration.GetSection(key: "EndpointUri").Value;
string PrimaryKey = builder.Configuration.GetSection(key: "PrimaryKey").Value;
var dbName = "EmployeeDB";

builder.Services.AddDbContext<AppDbContext>(option => option.UseCosmos(EndpointUri, PrimaryKey, databaseName: dbName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();
app.Run();



