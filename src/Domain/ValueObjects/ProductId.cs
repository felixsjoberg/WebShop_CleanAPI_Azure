namespace Domain.ValueObjects
{
    public class ProductId
    {
        public ProductId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

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