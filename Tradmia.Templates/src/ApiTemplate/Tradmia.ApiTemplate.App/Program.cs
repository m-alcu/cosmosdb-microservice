using Application.Data.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tradmia.ApiTemplate.App.Extensions;
using Tradmia.ApiTemplate.App.Middlewares;
using Tradmia.ApiTemplate.Application;
using Tradmia.ApiTemplate.Application.Data.Employees;
using Tradmia.ApiTemplate.Infrastructure;
using Tradmia.ApiTemplate.Infrastructure.Caching;
using Tradmia.ApiTemplate.Infrastructure.Events;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

const string CorsPolicy = nameof(CorsPolicy);

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureAppConfiguration(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("AppConfig");
    options.Connect(connectionString);
});

builder.Services.AddAzureAppConfiguration();

string connection = builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:Redis:Endpoint"];

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
    string connection = builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:Redis:Endpoint"];
    redisOptions.Configuration = connection;
});
builder.Services.AddSingleton<ICacheService, CacheService>();

builder.Services.AddSingleton<IEventPublisher>(sp =>
    new EventGridPublisherService(
        builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:EventGrid:Endpoint"],
        builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:EventGrid:Key"]
    )
);

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseCosmos(
        builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:CosmosDb:Endpoint"],
        builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:CosmosDb:Key"],
        builder.Configuration["ca-tradmia-ApiTemplate-westeu-001:CosmosDb:Database"]
        )
);

var app = builder.Build();

_ = app.UseSwagger();
_ = app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tradmia.AccessControl.Api v1");
    options.RoutePrefix = string.Empty;
    options.DisplayOperationId();
    options.DisplayRequestDuration();
});

app.UseAzureAppConfiguration();

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



