using CoreApi.Domain.Primitives;

namespace CoreApi.Domain.Model;

public class Order : Entity
{
    public Order(Guid id) : base(id)
    {
    }

    public int? TrackingNumber { get; set; }
    public string PartitionKey { get; set; }
    public StreetAddress ShippingAddress { get; set; }
}
