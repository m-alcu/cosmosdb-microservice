namespace CoreApi.Domain.Exceptions

{
    public sealed class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(int id)
            : base($"The order with identifier {id} was not found")
        { }

    }
}
