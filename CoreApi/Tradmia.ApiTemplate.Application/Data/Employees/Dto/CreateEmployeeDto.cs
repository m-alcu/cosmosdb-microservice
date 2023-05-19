namespace Tradmia.ApiTemplate.Application.Data.Employees.Dto;

/// <summary>
/// Employee dto
/// </summary>
public class CreateEmployeeDto
{

    /// <summary>
    /// Employee's name
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

    /// <summary>
    /// Employee's dni
    /// </summary>
    public string? Dni { get; set; }

    /// <summary>
    /// Employee's email
    /// </summary>
    public string? Email { get; set; }
}
