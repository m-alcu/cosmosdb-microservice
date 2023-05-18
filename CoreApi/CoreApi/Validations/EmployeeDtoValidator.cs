using Application.Data.Employees.Dto;
using FluentValidation;

namespace Tradmia.Ciutada.Api.Application.Validations;

/// <summary>
/// EmployeeDto validator
/// </summary>
public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public EmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MinimumLength(3);
        RuleFor(x => x.Surname).NotNull().MinimumLength(3);
        RuleFor(x => x.Dni).NotNull().MinimumLength(8).MaximumLength(12);
        RuleFor(x => x.Email).NotNull().EmailAddress();
    }
}
