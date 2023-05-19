using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Tradmia.ApiTemplate.Application.Data.Employees.Dto;
using Tradmia.ApiTemplate.Domain.Entities.Employees;

namespace Tradmia.ApiTemplate.Application;

/// <summary>
/// Mapper Profile
/// </summary>
[ExcludeFromCodeCoverage]
public class MapperProfile : Profile
{
    /// <summary>
    /// Constructor
    /// </summary>
    public MapperProfile()
    {
        CreateMap<EmployeeDto, Employee>().ReverseMap();
        CreateMap<UpdateEmployeeDto, Employee>();
        CreateMap<CreateEmployeeDto, Employee>();
    }
}
