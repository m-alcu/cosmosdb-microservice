using Domain.Orders;

namespace Domain.UnitTests.Orders;

public class SkuTests
{

    // [ThingUNderTest]_Should_[ExpectedResult]_[Conditions]
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_ReturnNull_WhenValueIsNullOrEmpty(string? value)
    {
        //Act
        var sku = Sku.Create(value);

        //Assert
        Assert.Null(sku);

    }


    //Static Object
    public static IEnumerable<object[]> InvalidSkuLenghData = new List<object[]>
    {
        new object[] { "invalid_sku" },
        new object[] { "invalid_sku_1" },
        new object[] { "invalid_sku_2" }
    };

    //new instance every time the property is accessed
    //public static property
    public static IEnumerable<object[]> InvalidSkuLenghDataProp => new List<object[]>
    {
        new object[] { "invalid_sku" },
        new object[] { "invalid_sku_1" },
        new object[] { "invalid_sku_2" }
    };

    //public static method
    //Aqui podriamos poner un for loop y poner varios casos, este es el mejor de todos
    public static IEnumerable<object[]> InvalidSkuLenghDataMwethod() => new List<object[]>
    {
        new object[] { "invalid_sku" },
        new object[] { "invalid_sku_1" },
        new object[] { "invalid_sku_2" }
    };


    public static IEnumerable<object[]> InvalidSkuLenghDataMethod2()
    {
        var data = new List<object[]>();

        for (int i = 0; i < 5; i++)
        {
            data.Add(new object[] { $"invalid_sku_{i}" });
        }

        return data;
    }


    [Theory]
    [MemberData(nameof(InvalidSkuLenghDataMethod2))]
    public void Create_Should_ReturnNull_WhenValueIsInvalid(string? value)
    {
        var sku = Sku.Create(value);

        Assert.Null(sku);
    }

}
