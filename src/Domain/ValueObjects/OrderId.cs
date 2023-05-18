namespace Domain.ValueObjects
{
    public class OrderId
    {
        public OrderId(Guid? value)
        {
            Value = value;
        }

        public Guid? Value { get; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Value.Equals(((OrderId)obj).Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}