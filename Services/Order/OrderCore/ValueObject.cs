namespace OrderCore;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }
    
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !(EqualOperator(left, right));
    }
    
    
    protected abstract IEnumerable<object> GetEqualityComponents();
    
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is ValueObject))
            return false;
        
        var other = (ValueObject)obj;
        
        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
    
    public override int GetHashCode()
    {
        return this.GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + ((obj != null) ? obj.GetHashCode() : 0);
                    }
                });
    }
    
    public ValueObject GetCopy()
    {
        return this.MemberwiseClone() as ValueObject;
    }
    
    
    
    
    
}