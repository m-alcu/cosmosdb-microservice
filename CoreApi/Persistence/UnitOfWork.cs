using Application.Data.Employees;
using CoreApi.Domain.Exceptions;
using CoreApi.Infrastructure.Database;
using Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreApi.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ILogger<EmployeeService> _logger;

    public UnitOfWork(ILogger<EmployeeService> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }



    public async Task<Guid> CreateEmployee(Employee employee)
    {
        await _context.Database.EnsureCreatedAsync();

        Guid guid = Guid.NewGuid();

        _context.Add(
            new Employee(guid, employee.TenantId, employee.Name, employee.Surname, employee.Email, employee.Dni));

        await _context.SaveChangesAsync();

        _logger.LogInformation("Created Guid {name}!", guid);

        return guid;
    }

    public async Task<Employee?> DeleteEmployee(Guid id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(o => o.Id == id);
        if (employee is null)
        {
            return employee;
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken cancellationToken)
    {
        await _context.Database.EnsureCreatedAsync();

        return await _context.Employees.ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetEmployeeById(Guid id)
    {
        return await _context.Employees.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(o => o.Id == updatedEmployee.Id);

        if (employee is null)
        {
            throw new DomainException("Employee " + updatedEmployee.Id + " does not exist");
        }

        employee.UpdateEmployee(updatedEmployee.TenantId, updatedEmployee.Name, updatedEmployee.Surname, updatedEmployee.Email, updatedEmployee.Dni);

        _context.Update(employee);
        await _context.SaveChangesAsync();

        return employee;
    }
    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
