using Probabilistic.Structures.BloomFilterImpl.Base;

namespace Probabilistic.Structures.BloomFilterImpl;

#nullable disable
public class BloomFilter<T>
{
    private readonly object _syncRoot = new();

    private bool _isLocked = false;
    private int _m;
    private int _k;

    private uint[] _seeds;
    private Bucket[] _buckets;

    public BloomFilter(double errorRate, int capacity)
    {
        Reset(errorRate, capacity);
    }

    public void Reset(double errorRate, int capacity)
    {
        lock (_syncRoot)
        {
            _m = (int)Math.Ceiling((capacity * Math.Log(errorRate)) / Math.Log(1 / Math.Pow(2, Math.Log(2))));
            _k = (int)Math.Ceiling((_m / capacity) * Math.Log(2));

            _seeds = Enumerable.Range(0, _k).Select(_ => (uint)Guid.NewGuid().GetHashCode()).ToArray();
            _buckets = Enumerable.Range(0, _m).Select(_ => new Bucket()).ToArray();
        }
    }

    public void Reset()
    {
        lock (_syncRoot)
        {
            _seeds = Enumerable.Range(0, _k).Select(_ => (uint)Guid.NewGuid().GetHashCode()).ToArray();
            _buckets = Enumerable.Range(0, _m).Select(_ => new Bucket()).ToArray();
        }
    }

    public void Add(T value)
    {
        lock (_syncRoot)
        {
            for (var i = 0; i < _k; i++)
            {
                GetBucket(value, i).Set();
            }
        }
    }

    public bool Exists(T value)
    {
        for (var i = 0; i < _k; i++)
        {
            if (!GetBucket(value, i).IsSet)
                return false;
        }

        return true;
    }

    private Bucket GetBucket(T data, int seedIndex) =>
        _buckets[GetFingerprint(data, seedIndex) % _m];

    private uint GetFingerprint(T data, int seedIndex) =>
        (uint)data!.GetHashCode() + _seeds[seedIndex];
}

