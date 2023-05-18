namespace Domain.UnitTests.Employees;

public class EmployeeTests
{

    [Theory]
    [ClassData(typeof(EmployeeCreateTestData))]
    public void Create_Should_NotBeEmppty(Guid id, int? trackingNumber, StreetAddress shippingAddress)
    {
        var Employee = new Employee(id, trackingNumber, shippingAddress);

        Assert.NotEmpty(Employee.ShippingAddress.City);

    }
}

public class EmployeeCreateTestData : TheoryData<Guid, int, StreetAddress>
{
    public EmployeeCreateTestData()
    {

        Add(new Guid(), 1, new StreetAddress { Street = "sss", City = "ssss" });
    }
}