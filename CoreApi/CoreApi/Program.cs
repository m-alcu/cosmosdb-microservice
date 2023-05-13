using CoreApi.Application.Middlewares;
using CoreApi.Application.Services;
using CoreApi.Infrastructure.Database;
using CoreApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddScoped<ApplicationContext>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


string EndpointUri = builder.Configuration.GetSection(key: "EndpointUri").Value;
string PrimaryKey = builder.Configuration.GetSection(key: "PrimaryKey").Value;
var dbName = "OrdersDB";

builder.Services.AddDbContext<ApplicationContext>(option => option.UseCosmos(EndpointUri, PrimaryKey, databaseName: dbName));

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



