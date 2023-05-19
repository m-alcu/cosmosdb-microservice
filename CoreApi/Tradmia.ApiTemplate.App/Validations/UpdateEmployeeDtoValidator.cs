using FluentValidation;
using Tradmia.ApiTemplate.Application.Data.Employees.Dto;

namespace Tradmia.ApiTemplate.App.Validations;

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
