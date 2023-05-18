namespace Domain.ValueObjects
{
    public class ProductId
    {
        public ProductId(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value.Equals(((ProductId)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}