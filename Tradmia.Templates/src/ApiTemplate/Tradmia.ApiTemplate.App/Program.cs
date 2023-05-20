using Application.Data.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tradmia.ApiTemplate.App.Middlewares;
using Tradmia.ApiTemplate.Application;
using Tradmia.ApiTemplate.Application.Caching;
using Tradmia.ApiTemplate.Application.Data.Employees;
using Tradmia.ApiTemplate.Infrastructure;
using Tradmia.Ciutada.Api.Extensions;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

const string CorsPolicy = nameof(CorsPolicy);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
_ = builder.Services.AddEndpointsApiExplorer()
    .AddSwagger()
    .AddValidators()
    .AddCors(options => options.AddPolicy(CorsPolicy,
        policyBuilder => policyBuilder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("ContinuationToken", "TotalCount")));


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

_ = app.UseSwagger();
_ = app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradmia.AccessControl.Api v1");
    options.RoutePrefix = string.Empty;
    options.DisplayOperationId();
    options.DisplayRequestDuration();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
    _ = app.UseCors(t => t.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    _ = app.UseHttpLogging();
}

app.UseHttpsRedirection()
    .UseRouting()
    .UseCors(CorsPolicy);

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();
app.Run();



