using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using Tradmia.ApiTemplate.App.Validations;
using Tradmia.ApiTemplate.Application.Data.Employees.Dto;

namespace Tradmia.ApiTemplate.App.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add swagger doc
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        const string contactUrl = "http://www.inetum.com";
        const string contactName = "INETUM";
        const string docNameV1 = "v1";
        const string docInfoTitle = "TRADMIA ApiTemplate API";
        const string docInfoVersion = "v1";
        const string docInfoDescription = "TRADMIA ApiTemplate API - '{description here}'";

        // Register the Swagger generator, defining one or more Swagger documents
        services.AddSwaggerGen(swaggerGenOpts =>
        {
            var contact = new OpenApiContact { Name = contactName, Url = new Uri(contactUrl) };
            swaggerGenOpts.SwaggerDoc(docNameV1,
                new OpenApiInfo
                {
                    Title = docInfoTitle,
                    Version = docInfoVersion,
                    Description = docInfoDescription,
                    TermsOfService = new Uri(contactUrl),
                    Contact = contact
                });

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "Tradmia.ApiTemplate.App.xml");
            swaggerGenOpts.IncludeXmlComments(xmlPath);
            swaggerGenOpts.DescribeAllParametersInCamelCase();
            swaggerGenOpts.DocInclusionPredicate((_, _) => true);
            //swaggerGenOpts.OperationFilter<TenantHeaderParameterOperationFilter>();
        });

        return services;
    }

    /// <summary>
    /// Register validators
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        _ = services.AddTransient<IValidator<EmployeeDto>, EmployeeDtoValidator>()
            .AddTransient<IValidator<CreateEmployeeDto>, CreateEmployeeDtoValidator>()
            .AddTransient<IValidator<UpdateEmployeeDto>, UpdateEmployeeDtoValidator>();
        return services;
    }
}
