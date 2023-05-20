using FluentValidation;
using Tradmia.ApiTemplate.Application.Data.Employees.Dto;

namespace Tradmia.ApiTemplate.App.Validations;

/// <summary>
/// CreateEmployeeDto validator
/// </summary>
public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
{
    /// <summary>
    /// Constructor
    /// </summary>
    public CreateEmployeeDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MinimumLength(3);
        RuleFor(x => x.Surname).NotNull().MinimumLength(3);
        RuleFor(x => x.Dni).NotNull().MinimumLength(8).MaximumLength(12);
        RuleFor(x => x.Email).NotNull().EmailAddress();
    }
}
