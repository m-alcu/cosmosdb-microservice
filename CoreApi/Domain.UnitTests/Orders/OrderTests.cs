using Domain.Entities.Orders;

namespace Domain.UnitTests.Orders;

public class OrderTests
{

    [Theory]
    [ClassData(typeof(OrderCreateTestData))]
    public void Create_Should_NotBeEmppty(Guid id, int? trackingNumber, StreetAddress shippingAddress)
    {
        var order = new Order(id, trackingNumber, shippingAddress);

        Assert.NotEmpty(order.ShippingAddress.City);

    }
}

public class OrderCreateTestData : TheoryData<Guid, int, StreetAddress>
{
    public OrderCreateTestData()
    {

        Add(new Guid(), 1, new StreetAddress { Street = "sss", City = "ssss" });
    }
}