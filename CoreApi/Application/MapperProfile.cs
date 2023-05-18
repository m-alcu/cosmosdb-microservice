using Application.Data.Employees.Dto;
using AutoMapper;
using Domain.Entities.Employees;
using System.Diagnostics.CodeAnalysis;

namespace Tradmia.Ciutada.Application;

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
