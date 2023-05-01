using CoreApi.Domain.Primitives;
using System.Text.Json.Serialization;

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

    public void UpdateOrder(int? trackingNumber, StreetAddress shippingAddress)
    {
        TrackingNumber = trackingNumber;
        ShippingAddress = shippingAddress;
    }

    public int? TrackingNumber { get; private set; }
    [JsonIgnore]
    public string PartitionKey { get; private set; }
    public StreetAddress ShippingAddress { get; private set; }
}
