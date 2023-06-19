using Probabilistic.Structures.BloomFilterImpl.Base;

namespace Probabilistic.Structures.BloomFilterImpl;

#nullable disable
public class BloomFilter<T>
{
    private readonly uint[] _seeds;
    private readonly int _m;
    private readonly int _k;

    private Bucket[] _buckets;

    public BloomFilter(double errorRate, int capacity)
    {
        _m = (int)Math.Ceiling((capacity * Math.Log(errorRate)) / Math.Log(1 / Math.Pow(2, Math.Log(2))));
        _k = (int)Math.Ceiling((_m / capacity) * Math.Log(2));

        _seeds = Enumerable.Range(0, _k).Select(_ => (uint)Guid.NewGuid().GetHashCode()).ToArray();
        Reset();
    }

    public void Reset()
    {
        _buckets = Enumerable.Range(0, _m).Select(_ => new Bucket()).ToArray();
    }

    public void Add(T data)
    {
        for (var i = 0; i < _k; i++)
        {
            GetBucket(data, i).Set();
        }
    }

    public bool Exists(T data)
    {
        for (var i = 0; i < _k; i++)
        {
            if (!GetBucket(data, i).IsSet)
                return false;
        }

        return true;
    }

    private Bucket GetBucket(T data, int seedIndex) =>
        _buckets[GetFingerprint(data, seedIndex) % _m];

    private uint GetFingerprint(T data, int seedIndex) =>
        (uint)data!.GetHashCode() + _seeds[seedIndex];
}

