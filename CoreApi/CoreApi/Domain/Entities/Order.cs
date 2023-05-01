using CoreApi.Domain.Primitives;

namespace CoreApi.Domain.Model;

public class Order : Entity
{
    protected Order() { }
    public Order(Guid id, int? trackingNumber, StreetAddress shippingAddress) : base(id)
    {
        TrackingNumber = trackingNumber;
        PartitionKey = shippingAddress.City;
        ShippingAddress = shippingAddress;
    }

    public int? TrackingNumber { get; private set; }
    public string PartitionKey { get; private set; }
    public StreetAddress ShippingAddress { get; private set; }
}
