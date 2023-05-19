namespace Tradmia.ApiTemplate.Application.Data.Employees.Dto;

/// <summary>
/// Employee dto
/// </summary>
public class UpdateEmployeeDto
{
    /// <summary>
    /// Employee's id
    /// </summary>
    public Guid? Id { get; set; }


    /// <summary>
    /// Employee's id
    /// </summary>
    public string? TenantId { get; set; }

    /// <summary>
    /// Employee's name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Employee's surname
    /// </summary>
    public string? Surname { get; set; }
}
