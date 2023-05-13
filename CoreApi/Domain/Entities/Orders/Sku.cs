namespace Domain.Entities.Orders
{
    public record Sku
    {
        private const int DefaultLengh = 15;

        public Sku(string value) => Value = value;

        public string Value { get; set; }

        public static Sku? Create(string? value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            if (value.Length != DefaultLengh) return null;

            return new Sku(value);
        }
    }
}
