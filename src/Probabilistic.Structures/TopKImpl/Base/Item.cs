namespace Probabilistic.Structures.TopKImpl.Base
{
    public struct Item<T>
        where T : IEquatable<T>
    {
        private T _value;
        private long _count;

        public readonly T Data => _value;
        public readonly long Count => _count;

        internal Item(T value, long count) =>
            (_value, _count) = (value, count);

        internal readonly bool Is(T value) =>
            Data.Equals(value);

        internal void Increment() =>
            _count++;

        internal void Set(T value, long count) =>
            (_value, _count) = (value, count);

        public override readonly string ToString()
        {
            return $"Value: {Data} | Count: {Count}";
        }
    }
}
