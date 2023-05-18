using Application.Data.Employees.Dto;
using AutoMapper;
using CoreApi.Application.Exceptions;
using CoreApi.Infrastructure.Database;
using Domain.Entities.Employees;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Employees;

/// <summary>
/// Employee app service implementation
/// </summary>
public class EmployeeAppService : IEmployeeAppService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    private readonly DbSet<Employee> _employees;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="appDbContext"><see cref="AppDbContext"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public EmployeeAppService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _employees = appDbContext.Set<Employee>();
    }


    /// <inheritdoc cref="IEmployeeAppService"/>
    public async Task<List<EmployeeDto>> GetAsync()
    {
        await _appDbContext.Database.EnsureCreatedAsync();

        List<Employee> employees = await _employees.ToListAsync();
        return _mapper.Map<List<EmployeeDto>>(employees);
    }

    /// <inheritdoc cref="IEmployeeAppService"/>
    public async Task<EmployeeDto> GetByIdAsync(Guid id)
    {
        Employee? employee = await _employees.FirstOrDefaultAsync(o => o.Id == id);
        return _mapper.Map<EmployeeDto>(employee);
    }

    /// <inheritdoc cref="IEmployeeAppService"/>
    public async Task<Guid> CreateAsync(CreateEmployeeDto employee)
    {
        Guid guid = Guid.NewGuid();

        _ = _employees.Add(
    new Employee(guid, employee.TenantId, employee.Name, employee.Surname, employee.Email, employee.Dni));
        _ = await _appDbContext.SaveChangesAsync();

        return guid;
    }

    /// <inheritdoc cref="IEmployeeAppService"/>
    public Task UpdateAsync(EmployeeDto employee)
    {
        Employee employeeToUpdate = _mapper.Map<Employee>(employee);
        _appDbContext.Entry(employeeToUpdate).State = EntityState.Modified;
        return _appDbContext.SaveChangesAsync();
    }

    /// <inheritdoc cref="IEmployeeAppService"/>
    public async Task PatchAsync(UpdateEmployeeDto employee)
    {
        Employee? employeeInDb = await _employees.FirstOrDefaultAsync(o => o.Id == employee.Id);
        if (employeeInDb is null)
        {
            throw new NotFoundException();
        }
        _ = _mapper.Map(employee, employeeInDb);
        _ = await _appDbContext.SaveChangesAsync();
    }

    /// <inheritdoc cref="IEmployeeAppService"/>
    public async Task DeleteAsync(Guid id)
    {
        Employee? employee = await _employees.FirstOrDefaultAsync(o => o.Id == id);

        if (employee is null)
        {
            throw new NotFoundException();
        }
        _ = _employees.Remove(employee);
        _ = await _appDbContext.SaveChangesAsync();
    }

}
