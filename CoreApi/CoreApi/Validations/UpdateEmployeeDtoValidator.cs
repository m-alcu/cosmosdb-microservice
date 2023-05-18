using Application.Data.Employees.Dto;
using FluentValidation;

namespace Tradmia.Ciutada.Api.Application.Validations;

/// <summary>
/// UpdateEmployeeDto validator
/// </summary>
public class UpdateEmployeeDtoValidator : AbstractValidator<UpdateEmployeeDto>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public UpdateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MinimumLength(3);
        RuleFor(x => x.Surname).NotNull().MinimumLength(3);
    }
}
