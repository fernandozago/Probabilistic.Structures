namespace Probabilistic.Structures.HeavyKeeperImpl;

public class Item<T>
    where T : IEquatable<T>
{
    private T _value;
    private long _count;

    public T Data => _value;
    public long Count => _count;

    internal Item(T value, long count) =>
        (_value, _count) = (value, count);

    internal bool Is(T value) =>
        Data.Equals(value);

    internal void Increment() =>
        _count++;

    internal void Set(T value, long count) =>
        (_value, _count) = (value, count);

    public override string ToString()
    {
        return $"{{ Value: '{Data}', Count: {Count} }}";
    }
}