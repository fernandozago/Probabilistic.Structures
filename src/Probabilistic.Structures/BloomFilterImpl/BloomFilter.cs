using Probabilistic.Structures.BloomFilterImpl.Base;

namespace Probabilistic.Structures.BloomFilterImpl;

public class BloomFilter<T>
{
    private readonly Bucket[] _buckets;
    private readonly uint[] _seeds;
    private readonly Random _random =
#if NET5_0
        new(Guid.NewGuid().GetHashCode());
#else
        Random.Shared;
#endif

    public BloomFilter(double errorRate, int items)
    {
        int m = (int)Math.Ceiling((items * Math.Log(errorRate)) / Math.Log(1 / Math.Pow(2, Math.Log(2))));
        int k = (int)Math.Ceiling((m / items) * Math.Log(2));

        _buckets = Enumerable.Range(0, m).Select(_ => new Bucket()).ToArray();
        _seeds = Enumerable.Range(0, k).Select(_ => (uint)Guid.NewGuid().GetHashCode() + (uint)_random.Next(0, k)).ToArray();
    }

    public void Reset()
    {
        for (var i = 0; i < _buckets.Length; i++)
        {
            _buckets[i].Reset();
        }
    }

    public void Add(T data)
    {
        for (var i = 0; i < _seeds.Length; i++)
        {
            GetBucket(data, i).Set();
        }
    }

    public bool Exists(T data)
    {
        for (var i = 0; i < _seeds.Length; i++)
        {
            if (!GetBucket(data, i).IsSet)
                return false;
        }

        return true;
    }

    private Bucket GetBucket(T data, int seedIndex) =>
        _buckets[GetFingerprint(data, seedIndex) % _buckets.Length];

    private uint GetFingerprint(T data, int seedIndex) =>
        (uint)data!.GetHashCode() + _seeds[seedIndex];
}

