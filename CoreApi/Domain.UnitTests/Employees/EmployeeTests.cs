using Tradmia.ApiTemplate.Domain.Entities.Employees;

namespace Tradmia.ApiTemplate.UnitTests.Employees;

public class EmployeeTests
{

    [Theory]
    [ClassData(typeof(EmployeeCreateTestData))]
    public void Create_Should_NotBeEmppty(Guid id, string tenantId, string? name, string? surname, string? email, string? dni)
    {
        var employee = new Employee(id, tenantId, name, surname, email, dni);

        Assert.NotEmpty(employee.Name);

    }
}

public class EmployeeCreateTestData : TheoryData<Guid, string, string, string, string, string>
{

    public EmployeeCreateTestData()
    {

        Add(new Guid(), "1", "name", "surname", "email", "dni");
    }
}