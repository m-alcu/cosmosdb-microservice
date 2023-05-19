using Application.Data.Employees.Dto;

namespace Application.Data.Employees;

/// <summary>
/// Employee application service definition
/// </summary>
public interface IEmployeeAppService
{
    /// <summary>
    /// Gets a collection of employees.
    /// </summary>
    /// <returns>List of <see cref="EmployeeDto"/></returns>
    Task<List<EmployeeDto>> GetAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets the employee with the specified 'id'.
    /// </summary>
    /// <param name="id">Employee 'id'</param>
    /// <returns><see cref="EmployeeDto"/> with 'id' or null></returns>
    Task<EmployeeDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a new employee.
    /// </summary>
    /// <param name="employee">Employee to create</param>
    /// <returns>Created <see cref="Guid"/></returns>
    Task<Guid> CreateAsync(CreateEmployeeDto employee);

    /// <summary>
    /// Updates an existing employee with the specified 'id'.
    /// </summary>
    /// <param name="employee">Employee to update</param>
    Task UpdateAsync(EmployeeDto employee);

    /// <summary>
    /// Partially updates an employee 
    /// </summary>
    /// <param name="employee">Employee to update</param>
    Task PatchAsync(UpdateEmployeeDto employee);

    /// <summary>
    /// Deletes the employee with the specified 'id'.
    /// </summary>
    /// <param name="id"></param>
    Task DeleteAsync(Guid id);
}
